using System.Threading;
using System.Threading.Tasks;
using HiveMind.Server.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HiveMind.Server.HostedServices;

public class SchedulingBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<SchedulingBackgroundService> _logger;

    public SchedulingBackgroundService(IServiceProvider serviceProvider, ILogger<SchedulingBackgroundService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // Create a new scope for database operations
            using (var scope = _serviceProvider.CreateScope())
            {
                var stationService = scope.ServiceProvider.GetRequiredService<StationService>();

                var programStrategyService = scope.ServiceProvider.GetRequiredService<ProgramStrategyService>();

                _logger.LogInformation("SchedulingBackgroundService is running at: {time}", DateTimeOffset.Now);
                _logger.LogInformation("Found {Count} Stations", stationService.GetAllStations().Count());

                // Need to figure out how to figure out if we need even create a schedule.

                /*
                 *  Get check all active programs. 
                 *  
                 * 
                 * 
                 * 
                 * 
                 * 
                 * 
                 */
            }

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken); // Example delay
        }
    }

    private async Task CreateBatch()
    {

    }

    private async Task ProcessBatch()
    {

    }
}
