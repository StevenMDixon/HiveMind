using FFMpegCore;
using HiveMind.Server.Domain.Importer;
using HiveMind.Server.Services;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace HiveMind.Server.HostedServices;

public partial class MediaImporterBackgroundService(IServiceProvider serviceProvider, ILogger<MediaImporterBackgroundService> logger) : BackgroundService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly ILogger<MediaImporterBackgroundService> _logger = logger;
    private readonly string fileFormats = "mp4|avi|mkv|mov|wmv|flv|webm|m4v";

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // Create a new scope for database operations
            using (var scope = _serviceProvider.CreateScope())
            {
                var libraryService = scope.ServiceProvider.GetRequiredService<LibraryService>();
                var mediaItemService = scope.ServiceProvider.GetRequiredService<Services.MediaItemService>();
                var settingsService = scope.ServiceProvider.GetRequiredService<SettingsService>();
                
                var unprocessedLibraries = libraryService.GetUnprocessedLibraries();

                _logger.LogInformation("MediaImporterBackgroundService is running at: {time}", DateTimeOffset.Now);
                _logger.LogInformation("Found {Count} Unprocessed Libraries", unprocessedLibraries.Count());

                var targetLibary = unprocessedLibraries.FirstOrDefault();

                var mediaItems = new List<VideoMeta>();
                var mediaItemsToDelete = new List<Entities.MediaItem>();

                var settings = settingsService.GetAllSettings().ToDictionary(x => x.Name, y => y.Value);

                var mountedPath = settings["Import_Location"] ?? "";

                if (targetLibary != null && !targetLibary.IsProcessed)
                {
                    var currentMediaItems = mediaItemService.GetMediaItemLibraryID(targetLibary.LibraryId);

                    if (targetLibary.LibraryPath != null && targetLibary.LibraryPath != string.Empty)
                    {

                        //targetLibary.LibraryPath = mountedPath + targetLibary.LibraryPath;

                        _logger.LogInformation("Processing Library: {LibraryName}", targetLibary.LibraryName);

                        var files = new List<string>();

                        try
                        {
                            var pathsToIgnore = targetLibary.PathsToIgnore == string.Empty ? [] : targetLibary.PathsToIgnore.Split(';');

                            files.AddRange(
                                Directory.GetFiles(mountedPath + targetLibary.LibraryPath, "*.*", SearchOption.AllDirectories)
                                .Where(x => Regex.IsMatch(x, $".*[.]({fileFormats})$"))
                                .Where(x => pathsToIgnore.Length == 0 || !pathsToIgnore.Any(path => x.Contains(path)))
                                );
                        }
                        catch (Exception e)
                        {
                            _logger.LogError(e, "Error while getting files from library path: {LibraryPath}", targetLibary.LibraryPath);
                        }

                        //_logger.LogInformation("Found files: {Count}", files.Count);

                        mediaItemsToDelete = [.. currentMediaItems.ExceptBy(files, x => x.FilePath)];

                        var importer = ImporterFactory.Resolve(targetLibary.LibraryType);

                        var tagService = scope.ServiceProvider.GetRequiredService<TagsService>();
                        var showService = scope.ServiceProvider.GetRequiredService<ShowService>();
                        var queryService = scope.ServiceProvider.GetRequiredService<QueryService>();

                        var importerResults = importer.Generate([.. files.Where(x => !currentMediaItems.Any(y => y.FilePath == x))], mountedPath, targetLibary.LibraryPath, showService, tagService, queryService);

                        foreach (var importerResult in importerResults)
                        {
                            var mediaInfo = await FFProbe.AnalyseAsync(importerResult.FullPath);

                            if (mediaInfo.PrimaryVideoStream != null)
                            {
                                importerResult.Duration = mediaInfo.Duration.TotalMilliseconds;
                                importerResult.Height = mediaInfo.VideoStreams[0].Height;
                                importerResult.Width = mediaInfo.VideoStreams[0].Width;
                                importerResult.Resolution = mediaInfo.VideoStreams[0].DisplayAspectRatio.ToString();
                            }
                            else
                            {
                                _logger.LogInformation("Skipping file: {FilePath}: No video stream", importerResult.Path);
                            }

                            var needsCropping = await DetectCropAsync(importerResult.FullPath, stoppingToken);
                            
                            if(needsCropping != null && needsCropping.X > 40) importerResult.HasBlackBars = true;

                            mediaItems.Add(importerResult);
                        }

                        //if (mountedPath != String.Empty) targetLibary.LibraryPath = targetLibary.LibraryPath.Replace(mountedPath, "");
                    }

                    mediaItemService.AddMediaItems(mediaItems.Select(x => ConvertMetaToMediaItem(x, targetLibary.LibraryId)));    

                    if(mediaItemsToDelete.Count != 0)
                    {
                        mediaItemService.DeleteMany(mediaItemsToDelete);
                    }

                    libraryService.MarkLibraryAsProcessed(targetLibary);
                    _logger.LogInformation("Finished processing Library: {LibraryName}", targetLibary.LibraryName);
                }
            }

            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); // Example delay
        }
    }

    private static Entities.MediaItem ConvertMetaToMediaItem(VideoMeta meta, int libraryId)
    {
        return new Entities.MediaItem
        {
            Title = meta.Name,
            Duration = (int)meta.Duration,
            Width = meta.Width,
            Height = meta.Height,
            Resolution = meta.Resolution,
            ShowId = meta.ShowId,
            EpisodeNumber = meta.EpisodeNumber,
            SeasonNumber = meta.SeasonNumber,
            FilePath = meta.Path,
            LibraryId = libraryId,
            Tags = meta.Tags,
            HasBlackBars = meta.HasBlackBars
        };
    }

    public sealed record CropSettings(
    int Width,
    int Height,
    int X,
    int Y);

    public async Task<CropSettings?> DetectCropAsync(
    string filePath,
    CancellationToken cancellationToken = default)
    {
        var info = await FFProbe.AnalyseAsync(filePath);

        var video = info.VideoStreams.FirstOrDefault();

        if (video == null)
            return null;

        var duration = video.Duration;

        var sampleDuration = TimeSpan.FromSeconds(
            Math.Min(20, duration.TotalSeconds));

        var start = TimeSpan.FromSeconds(
            Math.Max(
                0,
                duration.TotalSeconds / 2 -
                sampleDuration.TotalSeconds / 2));

        var psi = new ProcessStartInfo
        {
            FileName = GlobalFFOptions.GetFFMpegBinaryPath(),
            UseShellExecute = false,
            RedirectStandardError = true,
            RedirectStandardOutput = true,
            CreateNoWindow = true
        };

        psi.ArgumentList.Add("-ss");
        psi.ArgumentList.Add(
            start.TotalSeconds.ToString(
                System.Globalization.CultureInfo.InvariantCulture));

        psi.ArgumentList.Add("-i");
        psi.ArgumentList.Add(filePath);

        psi.ArgumentList.Add("-t");
        psi.ArgumentList.Add(
            sampleDuration.TotalSeconds.ToString(
                System.Globalization.CultureInfo.InvariantCulture));

        psi.ArgumentList.Add("-vf");
        psi.ArgumentList.Add(
            "cropdetect=limit=40:round=2:reset=300," +
            "metadata=mode=print");

        psi.ArgumentList.Add("-f");
        psi.ArgumentList.Add("null");

        psi.ArgumentList.Add("-");

        using var process = new Process
        {
            StartInfo = psi
        };

        process.Start();

        var stderr = await process.StandardError.ReadToEndAsync(
            cancellationToken);

        await process.WaitForExitAsync(cancellationToken);

        return ParseCrop(stderr);
    }

    private static CropSettings? ParseCrop(string output)
    {
        var matches = CropRegex().Matches(output);

        if (matches.Count == 0)
            return null;

        // Last detection is the final crop recommendation.
        var match = matches[^1];

        return new CropSettings(
            int.Parse(match.Groups[1].Value),
            int.Parse(match.Groups[2].Value),
            int.Parse(match.Groups[3].Value),
            int.Parse(match.Groups[4].Value));
    }

    [GeneratedRegex(@"crop=(\d+):(\d+):(\d+):(\d+)")]
    private static partial Regex CropRegex();
}
