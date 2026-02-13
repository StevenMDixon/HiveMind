using System.Threading;
using System.Threading.Tasks;
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
                var dbContext = scope.ServiceProvider.GetRequiredService<sqliteDBContext>();

                // Perform database operations (e.g., logging, data processing)
                await dbContext.Database.EnsureCreatedAsync(stoppingToken);
                // ... further database logic
                _logger.LogInformation("SchedulingBackgroundService is running at: {time}", DateTimeOffset.Now);
                _logger.LogInformation("Found {Count} Channels", dbContext.Channels.Include(c => c.Schedule).Count());
            }

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken); // Example delay
        }
    }
}
