using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Entities;
using HiveMind.Server.Services;

namespace HiveMind.Server.HostedServices;

public class CleanupService(IServiceProvider serviceProvider, ILogger<CleanupService> logger) : BackgroundService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly ILogger<CleanupService> _logger = logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // Create a new scope for database operations
            using (var scope = _serviceProvider.CreateScope())
            {
                await CleanupSchedules(scope);
            }
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); // Example delay
        }
    }


    private static async Task CleanupSchedules(IServiceScope scope)
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
                if(File.Exists(result.Path))
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
}
