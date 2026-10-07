using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Domain.Scheduler;
using HiveMind.Server.Entities;
using HiveMind.Server.Services;

namespace HiveMind.Server.HostedServices;

public class SchedulingBackgroundService(IServiceScopeFactory scopeFactory, ILogger<SchedulingBackgroundService> logger) : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;
    private readonly ILogger<SchedulingBackgroundService> _logger = logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // Create a new scope for database operations
            using (var scope = _scopeFactory.CreateScope())
            {
                var libraryService = scope.ServiceProvider.GetRequiredService<LibraryService>();
                var unprocessedLibraries = libraryService.GetUnprocessedLibraries();

                if (unprocessedLibraries != null && unprocessedLibraries.Any())
                {
                    return; // Skip processing if there are unprocessed libraries
                }

                await CleanupSchedules(scope, stoppingToken);

                await ProcessBatch(scope, stoppingToken);
                await CreateBatches(scope, stoppingToken);
            }

            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); // Example delay
        }
    }

    private static async Task CleanupSchedules(IServiceScope scope, CancellationToken stoppingToken)
    {
        var settingsService = scope.ServiceProvider.GetRequiredService<SettingsService>();
        var batchService = scope.ServiceProvider.GetRequiredService<BatchService>();
        var scheduleService = scope.ServiceProvider.GetRequiredService<ScheduleService>();

        var retentionDaysSetting = settingsService.GetByName("Schedule Retention Days");
        if (retentionDaysSetting != null && int.TryParse(retentionDaysSetting.Value, out int retentionDays))
        {
            var cutoffDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-retentionDays));

            var oldSchedulingResults = scheduleService.GetSchedulingResults(cutoffDate);

            foreach (var result in oldSchedulingResults)
            {
                if (File.Exists(result.Path))
                {
                    File.Delete(result.Path);
                }

                scheduleService.Delete(result);
            }

            var oldBatchResults = batchService.GetBatchesBeforeDate(cutoffDate);

            var batchesToDelete = new List<ScheduleBatch>();

            foreach (var batch in oldBatchResults)
            {
                var batchChildren = batch?.ScheduleBatchItems?.All(item => item.ScheduleDate <= cutoffDate) ?? true;

                if (batch != null && batchChildren)
                {
                    batchesToDelete.Add(batch);
                }
            }

            batchService.DeleteBatchItems(batchesToDelete);
        }
    }

    private static async Task CreateBatches(IServiceScope scope, CancellationToken stoppingToken)
    {
        var programStrategyService = scope.ServiceProvider.GetRequiredService<ProgramStrategyService>();
        var batchService = scope.ServiceProvider.GetRequiredService<BatchService>();

        var programToProcess = programStrategyService.GetAvailableStrategiesToProcess();

        if (!programToProcess.Any())
        {
            //_logger.LogInformation("No Programs to Process");
            return;
        }
        // create batches for each program strategy that needs to be processed
        foreach (var program in programToProcess)
        {
            var batchItems = new List<ScheduleBatchItem>();

            var startDate = DateOnly.FromDateTime(DateTime.Now);
            var endDate = DateOnly.FromDateTime(DateTime.Now.AddDays(program.AdvancedDays));

            if (program.LastScheduleDate != null)
            {
                startDate = program.LastScheduleDate.Value.AddDays(1);
                // Probably want to do this so that if a schedule gets out of sync we still schedule those days
                // For Sequential items...?
            }

            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                batchItems.Add(new ScheduleBatchItem
                {
                    ScheduleDate = date,
                    IsCompleted = false,
                    OutputFilePath = program.Id.ToString(),
                    ProgramStrategyLineupId = LineUpSelectionMapper.GetMatchingProgramStrategyLineup(program, date)
                });
            }

            var batch = new ScheduleBatch
            {
                ProgramStrategyId = program.Id,
                ScheduleBatchItems = batchItems,
                StartDate = startDate,
                EndDate = endDate
            };

            batchService.AddBatch(batch);

            program.LastScheduleDate = endDate;

            programStrategyService.Update(program);
        }
    }

    private async Task ProcessBatch(IServiceScope scope, CancellationToken stoppingToken)
    {
        
        var batchService = scope.ServiceProvider.GetRequiredService<BatchService>();

        var openBatches = batchService.GetUnprocessedBatches();

        var batch = openBatches.FirstOrDefault();

        if (batch == null)
        {
            _logger.LogInformation("No open batches found.");
            return;
        }
        // each batch item represents a day in the overall schedule.
        var batchItemsToProcess = batch.ScheduleBatchItems?.Where(x => x.IsCompleted == false);

        var lineupService = scope.ServiceProvider.GetRequiredService<LineupService>();

        // Complete batch early if there are no items to process
        if (batchItemsToProcess == null || !batchItemsToProcess.Any())
        {
            batch.Status = BatchStatus.Completed;
            batchService.UpdateBatch(batch);
            _logger.LogWarning("Batch {BatchId} has no items to process.", batch.Id);
            return;
        }

        var programScheduleLineupService  = scope.ServiceProvider.GetRequiredService<ProgramStrategyLineupService>();

        // Process each batch item
        foreach (var batchItem in batchItemsToProcess)
        {
            if (batchItem == null)
            {
                _logger.LogWarning("Batch item is null for batch {BatchId}.", batch.Id);
                continue;
            }

            if(stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Cancellation requested. Stopping batch processing for batch {BatchId}.", batch.Id);
                return;
            }

            var lineup = programScheduleLineupService.GetProgramStrategyLineupById(batchItem.ProgramStrategyLineupId ?? 0);

            var scheduler = new Scheduler(scope.ServiceProvider);

            await scheduler.GenerateSchedule(lineup?.LineupId ?? 0, batch.ProgramStrategyId ?? 0, batchItem.ScheduleDate);

            // Mark the batch item as completed
            if (batchItem != null)
            {
                batchItem.IsCompleted = true;
                batchService.UpdateBatchItem(batchItem);
            }
        }

        return;
    }
}
