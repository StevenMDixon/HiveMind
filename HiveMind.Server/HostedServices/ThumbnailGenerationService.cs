using FFMpegCore;
using HiveMind.Server.Entities;
using HiveMind.Server.Services;
using System.Diagnostics;

namespace HiveMind.Server.HostedServices;

public class ThumbnailGenerationService(IServiceScopeFactory scopeFactory, ILogger<MediaImporterBackgroundService> logger) : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;

    private readonly ILogger<MediaImporterBackgroundService> _logger = logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var libraryService = scope.ServiceProvider.GetRequiredService<LibraryService>();
                var unprocessedLibraries = libraryService.GetUnprocessedLibraries();

                if (unprocessedLibraries != null && unprocessedLibraries.Any())
                {
                    return; // Skip processing if there are unprocessed libraries
                }

                var batchService = scope.ServiceProvider.GetRequiredService<BatchService>();
                var openBatches = batchService.GetUnprocessedBatches();
                var batch = openBatches.FirstOrDefault();

                if (batch != null)
                {
                    _logger.LogInformation("Skipping processing while batches are open");
                    return;
                }

                var mediaService = scope.ServiceProvider.GetRequiredService<MediaItemService>();
                var settingsService = scope.ServiceProvider.GetRequiredService<SettingsService>();
                var mediaItemsToProcess = mediaService.GetMediaItemsWithoutThumbs().Take(100);

                var settings = settingsService.GetAllSettings().ToDictionary(x => x.Name, y => y.Value);
                var mountedPath = settings["Import_Location"] ?? "";

                var outputPath = "./thumbs";

                var defaultPath = Path.GetFullPath(mountedPath);
                var outputP = Path.GetFullPath(outputPath);
                Directory.CreateDirectory(outputP);

                foreach (var item in mediaItemsToProcess)
                {
                    await Process(item, stoppingToken, defaultPath, outputP);
                    item.ThumbnailPath = $"{item.Id}.png";
                }

                mediaService.Update(mediaItemsToProcess);
            }
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); // Example delay
        }
    }

    public async Task Process(MediaItem mediaItems, CancellationToken stoppingToken, string path, string exportPath)
    {
        string timestamp = mediaItems.Duration / 1000 > 10 ? "00:00:10" : "00:00:05" ;
        string outPutPath = $"{exportPath + "/" +  mediaItems.Id}.png";

        string videoPath = Path.Combine(Path.Combine("/", path.TrimStart('\\', '/'), mediaItems.FilePath.TrimStart('\\', '/')));

        var psi = new ProcessStartInfo
        {
            FileName = GlobalFFOptions.GetFFMpegBinaryPath(),
            Arguments = $"-y -ss {timestamp} -i \"{videoPath}\" -frames:v 1 \"{outPutPath}\"",
            UseShellExecute = false,
            RedirectStandardError = true,
            RedirectStandardOutput = true,
            CreateNoWindow = true
        };

        using var process = new Process
        {
            StartInfo = psi
        };

        process.Start();

        var stderr = await process.StandardError.ReadToEndAsync(
            stoppingToken);

        Console.WriteLine(stderr);
    }
}
