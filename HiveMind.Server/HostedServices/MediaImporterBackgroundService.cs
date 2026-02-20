using System.Text.RegularExpressions;
using FFMpegCore;
using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Services;
using HiveMind.Server.Entities;


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

    private string fileFormats = "mp4|avi|mkv|mov|wmv|flv|webm|m4v";

    private record VideoMeta(string path, string name, double duration, int height, int width, string resolution, int? showId, int seasonNumber, int episodeNumber, ICollection<Tags> tags);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // Create a new scope for database operations
            using (var scope = _serviceProvider.CreateScope())
            {
                var libraryService = scope.ServiceProvider.GetRequiredService<LibraryService>();
                var mediaItemService = scope.ServiceProvider.GetRequiredService<MediaItemService>();
                var mediaItemShowService = scope.ServiceProvider.GetRequiredService<MediaItemShowService>();
                var tagService = scope.ServiceProvider.GetRequiredService<TagsService>();

                var unprocessedLibraries = libraryService.GetAllLibraries();
                _logger.LogInformation("MediaImporterBackgroundService is running at: {time}", DateTimeOffset.Now);
                _logger.LogInformation("Found {Count} Unprocessed Libraries", unprocessedLibraries.Count());

                var targetLibary = unprocessedLibraries.FirstOrDefault();

                var tagDict = tagService.GetAllTags().ToDictionary(t => t.TagName, t => t);

                var mediaItems = new List<VideoMeta>();
                var mediaItemsToDelete = new List<MediaItem>();

                if (targetLibary != null && !targetLibary.IsProcessed)
                {
                    // Get current media items so we can filter out any that have already been added to the database. We can use the file path to check if it has already been added.
                    var currentMediaItems = mediaItemService.GetMediaItemLibraryID(targetLibary.LibraryId);

                    if (targetLibary.LibraryPath != null)
                    {
                        _logger.LogInformation("Processing Library: {LibraryName}", targetLibary.LibraryName);

                        var files = Directory.GetFiles(targetLibary.LibraryPath, "*.*", SearchOption.AllDirectories).Where(x => Regex.IsMatch(x, $".*[.]({fileFormats})$"));

                        _logger.LogInformation("Found files: {Count}", files.Count());

                        mediaItemsToDelete = currentMediaItems.ExceptBy(files, x => x.FilePath).ToList();

                        var showCache = new Dictionary<string, MediaItemShow>();

                        foreach (string file in files)
                        {
                            
                            if(currentMediaItems.Any(x => x.FilePath == file))
                            {
                                _logger.LogInformation("Skipping file: {FileName}: Already exists in database", file);
                                continue;
                            }

                            _logger.LogInformation("Found file: {FileName}", file);

                            var mediaInfo = await FFProbe.AnalyseAsync(file);

                            var duration = mediaInfo.Duration.TotalMilliseconds;
                            var fileName = Path.GetFileNameWithoutExtension(file);
                            var fileNameWithExt = Path.GetFileName(file);
                            var width = 0;
                            var height = 0;
                            var res = "";

                            if(mediaInfo.PrimaryVideoStream != null)
                            {
                                height = mediaInfo.VideoStreams[0].Height;
                                width = mediaInfo.VideoStreams[0].Width;
                                res = mediaInfo.VideoStreams[0].DisplayAspectRatio.ToString();
                            } 
                            else
                            {
                                _logger.LogInformation("Skipping file: {FileName}: No video stream", fileName);
                                continue;
                            }

                            var tags = file.Replace(targetLibary.LibraryPath, String.Empty).Split('/').ToList().Where(x => x != "" && x != fileNameWithExt).ToList();

                            int seasonNumber = 0;
                            int? showId = null;
                            int episodeNumber = 0;

                            if (targetLibary.LibraryType == LibraryType.Show && tags.Count() > 0)
                            {
                                var formattedShowName = Regex.Replace(tags.First(), @"\s\(.*\)$", "").Trim();

                                if (showCache.ContainsKey(formattedShowName))
                                {
                                    showId = showCache[formattedShowName].MediaItemShowId;
                                }
                                else
                                {
                                    var existingShow = mediaItemShowService.GetByName(formattedShowName);
                                    if (existingShow != null)
                                    {
                                        showId = existingShow.MediaItemShowId;
                                        showCache[formattedShowName] = existingShow;
                                    }
                                    else
                                    {
                                        var newShow = new MediaItemShow { MediaItemShowTitle = formattedShowName };
                                        showId = mediaItemShowService.AddMediaItemShow(newShow);
                                        showCache[formattedShowName] = newShow;
                                    }
                                }
                                    
                                if (tags.Count() > 1)
                                {
                                    var formattedSeasonNumber = Regex.Replace(tags[1], @"[^0-9]", "");
                                    Int32.TryParse(formattedSeasonNumber, out seasonNumber);
                                }

                                var r = ExtractEpisodeMeta(fileName);
                                if (seasonNumber == 0 && r?.season != null) seasonNumber = r.season;
                                episodeNumber = r?.episodeStart ?? 0;
                            }

                            var mappedTags = new List<Tags>();

                            if (targetLibary.LibraryType != LibraryType.Show)
                            {
                                foreach (var tag in tags)
                                {
                                    // Check if already in dictionary
                                    if (tagDict.ContainsKey(tag))
                                    {
                                        mappedTags.Add(tagDict[tag]);
                                    }
                                    else
                                    {
                                        var newTag = new Tags { TagName = tag };
                                        tagDict.Add(tag, newTag);
                                        mappedTags.Add(newTag);
                                    }
                                }
                            }

                            mediaItems.Add(new VideoMeta(file, fileName, duration, height, width, res, showId, seasonNumber, episodeNumber, mappedTags));
                        }
                    }

                    mediaItemService.AddMediaItems(mediaItems.Select(x => ConvertMetaToMediaItem(x, targetLibary.LibraryId)));    

                    if(mediaItemsToDelete.Any())
                    {
                        mediaItemService.DeleteMany(mediaItemsToDelete);
                    }

                    libraryService.MarkLibraryAsProcessed(targetLibary);
                    _logger.LogInformation("Finished processing Library: {LibraryName}", targetLibary.LibraryName);
                }
            }

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken); // Example delay
        }
    }

    private MediaItem ConvertMetaToMediaItem(VideoMeta meta, int libraryId)
    {
        return new MediaItem
        {
            Title = meta.name,
            Duration = meta.duration,
            Width = meta.width,
            Height = meta.height,
            Resolution = meta.resolution,
            MediaItemShowId = meta.showId,
            EpisodeNumber = meta.episodeNumber,
            SeasonNumber = meta.seasonNumber,
            FilePath = meta.path,
            LibraryId = libraryId,
            Tags = meta.tags
        };
    }

    private record EpisodeMeta(int season, int episodeStart, int? episodeEnd);
    private static EpisodeMeta? ExtractEpisodeMeta(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            return null;

        // 1️⃣ SxxExx or SxxExx-Exx
        var sxeMatch = Regex.Match(
            fileName,
            @"\b[Ss](\d{1,2})[.\s_-]*[Ee](\d{1,2})(?:-(?:[Ee]?)?(\d{1,2}))?",
            RegexOptions.IgnoreCase);

        if (sxeMatch.Success)
        {
            int season = int.Parse(sxeMatch.Groups[1].Value);
            int epStart = int.Parse(sxeMatch.Groups[2].Value);

            int? epEnd = null;
            if (sxeMatch.Groups[3].Success)
                epEnd = int.Parse(sxeMatch.Groups[3].Value);

            return new EpisodeMeta(season, epStart, epEnd);
        }

        // 2️⃣ Compact 3-digit format (101)
        var compactMatch = Regex.Match(fileName, @"\b(\d)(\d{2})\b");

        if (compactMatch.Success)
        {
            return new EpisodeMeta(
                int.Parse(compactMatch.Groups[1].Value),
                int.Parse(compactMatch.Groups[2].Value),
                null
            );
        }

        // 3️⃣ Leading episode only (01 - Title)
        var leadingMatch = Regex.Match(fileName, @"^(\d{1,2})\s*-");

        if (leadingMatch.Success)
        {
            return new EpisodeMeta(
                0,
                int.Parse(leadingMatch.Groups[1].Value),
                null
            );
        }

        return null;
    }
}
