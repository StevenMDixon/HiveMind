using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Domain.Scheduler;
using HiveMind.Server.Entities;
using HiveMind.Server.Services;

namespace HiveMind.Server.HostedServices;

public class SchedulingBackgroundService(IServiceProvider serviceProvider, ILogger<SchedulingBackgroundService> logger) : BackgroundService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly ILogger<SchedulingBackgroundService> _logger = logger;

    public record ScheduleContext(DateOnly Date, ProgramStrategyLineup? ProgramStrategyLineup = null);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // Create a new scope for database operations
            using (var scope = _serviceProvider.CreateScope())
            {
                var batchService = scope.ServiceProvider.GetRequiredService<BatchService>();
                var libraryService = scope.ServiceProvider.GetRequiredService<LibraryService>();

                var unprocessedLibraries = libraryService.GetUnprocessedLibraries();

                if (unprocessedLibraries != null && unprocessedLibraries.Any())
                {
                    await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); // Example delay
                    continue; // Skip processing if there are unprocessed libraries
                }

                var openBatches = batchService.GetUnprocessedBatches();

                if (openBatches.Any())
                {
                    // Process first open batch
                    await ProcessBatch(openBatches.First(), scope);
                }
                else
                {
                    // we should not have any open batches at this point, so we can create new batches for any program strategies that need to be processed
                    var programStrategyService = scope.ServiceProvider.GetRequiredService<ProgramStrategyService>();
                    var programToProcess = programStrategyService.GetAvailableStrategiesToProcess();

                    if(programToProcess.Any())
                    {
                        // create batches for each program strategy that needs to be processed
                        foreach (var program in programToProcess)
                        {
                            await CreateBatch(program, batchService, programStrategyService);
                        }
                    }
                }
            }
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); // Example delay
        }
    }

    private static async Task CreateBatch(ProgramStrategy programStrategy, BatchService batchService, ProgramStrategyService programStrategyService)
    {
        var batchItems = new List<ScheduleBatchItem>();

        var startDate = DateOnly.FromDateTime(DateTime.Now);
        var endDate = DateOnly.FromDateTime(DateTime.Now.AddDays(programStrategy.AdvancedDays));

        if (programStrategy.LastScheduleDate != null)
        {
            startDate = programStrategy.LastScheduleDate.Value.AddDays(1);
            // Probably want to do this so that if a schedule gets out of sync we still schedule those days
            // For Sequential items...?
        }

        for (var date = startDate; date <= endDate; date = date.AddDays(1))
        {
            batchItems.Add(new ScheduleBatchItem
            {
                ScheduleDate = date,
                IsCompleted = false,
                OutputFilePath = programStrategy.ProgramStrategyId.ToString(),
                ProgramStrategyLineUpId = LineUpSelectionMapper.GetMatchingProgramStrategyLineup(programStrategy, date)
            });
        }

        var batch = new ScheduleBatch
        {
            ProgramStrategyId = programStrategy.ProgramStrategyId,
            ScheduleBatchItems = batchItems,
            StartDate = startDate,
            EndDate = endDate
        };

        batchService.AddBatch(batch);

        programStrategy.LastScheduleDate = endDate;

        programStrategyService.Update(programStrategy);
    }

    private async Task ProcessBatch(ScheduleBatch batch, IServiceScope scope)
    {
        // each batch item represents a day in the overall schedule.
        var batchItemsToProcess = batch.ScheduleBatchItems?.Where(x => x.IsCompleted == false);

        var batchService = scope.ServiceProvider.GetRequiredService<BatchService>();

        var lineupService = scope.ServiceProvider.GetRequiredService<LineupService>();

        // Complete batch early if there are no items to process
        if (batchItemsToProcess == null || !batchItemsToProcess.Any())
        {
            batch.Status = BatchStatus.Completed;
            batchService.UpdateBatch(batch);
            _logger.LogWarning("Batch {BatchId} has no items to process.", batch.ScheduleBatchId);
            return;
        }

        var programScheduleLineupService  = scope.ServiceProvider.GetRequiredService<ProgramStrategyLineupService>();

        // Process each batch item
        foreach (var batchItem in batchItemsToProcess)
        {
            if (batchItem == null)
            {
                _logger.LogWarning("Batch item is null for batch {BatchId}.", batch.ScheduleBatchId);
                continue;
            }
         
            var lineup = programScheduleLineupService.GetProgramStrategyLineupById(batchItem.ProgramStrategyLineUpId ?? 0);

            var scheduler = new Scheduler(scope);

            await scheduler.GenerateSchedule(lineup?.LineupId ?? 0, batch.ProgramStrategyId ?? 0, batchItem.ScheduleDate);

            // Mark the batch item as completed
            if (batchItem != null)
            {
                batchItem.IsCompleted = true;
                batchService.UpdateBatchItem(batchItem);
            }
        }
    }
}
