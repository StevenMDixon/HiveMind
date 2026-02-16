using System.Diagnostics;
using FFMpegCore;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;

namespace HiveMind.Server.HostedServices;

public class MediaImporterBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<MediaImporterBackgroundService> _logger;

    public MediaImporterBackgroundService(IServiceProvider serviceProvider, ILogger<MediaImporterBackgroundService> logger)
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
                var libraryService = scope.ServiceProvider.GetRequiredService<LibraryService>();

                var unprocessedLibraries = libraryService.GetAllLibraries();
                _logger.LogInformation("MediaImporterBackgroundService is running at: {time}", DateTimeOffset.Now);
                _logger.LogInformation("Found {Count} Unprocessed Libraries", unprocessedLibraries.Count());

                var targetLibary = unprocessedLibraries.FirstOrDefault();

                var directory = @"/home/app/test";

                if (targetLibary != null)
                {
                    if (targetLibary.LibraryPath != null)
                    {
                        _logger.LogInformation("Processing Library: {LibraryName}", targetLibary.LibraryName);
                        _logger.LogInformation("Path Exists: {Maybe}", Path.Exists(directory));
                        // Need to scan through whatever path is provided and find all media items.
                        var files = Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories);

                        _logger.LogInformation("Found files: {Count}", files.Count());

                        foreach (string file in files)
                        {
                            _logger.LogInformation("Found file: {FileName}", file);
                            // Here we would add the media item to the database and link it to the library
                            var mediaInfo = await FFProbe.AnalyseAsync(file);
                            _logger.LogInformation("Duration: {Duration}", mediaInfo.Duration);
                        }
                    }

                    libraryService.MarkLibraryAsProcessed(targetLibary);
                    _logger.LogInformation("Finished processing Library: {LibraryName}", targetLibary.LibraryName);
                }
            }

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken); // Example delay
        }
    }

    //private string GetVideoDuration(string filePath)
    //{
    //    string cmd = string.Format("-v error -select_streams v:0 -show_entries stream=duration -sexagesimal -of default=noprint_wrappers=1:nokey=1  {0}", filePath);
    //    Process proc = new Process();
    //    proc.StartInfo.FileName = @"ffprobe";
    //    proc.StartInfo.Arguments = cmd;
    //    proc.StartInfo.CreateNoWindow = true;
    //    proc.StartInfo.RedirectStandardOutput = true;
    //    proc.StartInfo.RedirectStandardError = true;
    //    proc.StartInfo.UseShellExecute = false;
    //    proc.StartInfo.UseShellExecute = false;
    //    if (!proc.Start())
    //    {
    //        Console.WriteLine("Error starting");
    //        return "";
    //    }
    //    string duration = proc.StandardOutput.ReadToEnd().Replace("\r\n", "");
    //    // Remove the milliseconds
    //    duration = duration.Substring(0, duration.LastIndexOf("."));
    //    proc.WaitForExit();
    //    proc.Close();

    //    return duration;
    //}
}
