using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Services;
using System.Text.RegularExpressions;
using static HiveMind.Server.Domain.Enums.QueryEnums;

namespace HiveMind.Server.Domain.Importer;

public partial class ShowImporter: IImporter
{
    public List<VideoMeta> Generate(List<string> files, string mountedPath, string libraryPath, ShowService showService, TagsService tagService, QueryService queryService)
    {
        var results = new List<VideoMeta>();

        var showDict = showService.GetAllShows().ToDictionary(t => t.ShowTitle, t => t);

        var newQueries = new Dictionary<string, HashSet<string>>();

        foreach (var file in files)
        {
            var fileName = Path.GetFileNameWithoutExtension(file);
            var normalizedFile = file.Replace(mountedPath, String.Empty);
            var fileNameWithExt = Path.GetFileName(file);

            var tags = file.Replace(mountedPath + libraryPath, String.Empty).Split('/').ToList().Where(x => x != "" && x != fileNameWithExt).ToList();

            int seasonNumber = 0;
            int? showId = null;
            int episodeNumber = 0;

            if (tags.Count > 0)
            {
                var formattedShowName = ShowRegex().Replace(tags.First(), "").Trim();

                if(!newQueries.ContainsKey(formattedShowName)) {
                    newQueries[formattedShowName] = [];
                }

                if (showDict.ContainsKey(formattedShowName))
                {
                    showId = showDict[formattedShowName].ShowId;
                }
                else
                {
                    var newShow = new Entities.Show { ShowTitle = formattedShowName };
                    showId = showService.AddShow(newShow);
                    showDict[formattedShowName] = newShow;
                }

                if (tags.Count > 1)
                {
                    var formattedSeasonNumber = SeasonNumber().Replace(tags[1], "");
                    var success = Int32.TryParse(formattedSeasonNumber, out seasonNumber);
                    if(!success) seasonNumber = 0;

                    newQueries[formattedShowName].Add(formattedSeasonNumber);
                }

                var r = ExtractEpisodeMeta(fileName);
                if (seasonNumber == 0 && r?.Season != null) seasonNumber = r.Season;
                episodeNumber = r?.EpisodeStart ?? 0;

                results.Add(new VideoMeta
                {
                    Name = fileName,
                    Path = normalizedFile,
                    FullPath = file,
                    ShowId = showId,
                    SeasonNumber = seasonNumber,
                    EpisodeNumber = episodeNumber
                });
            }
        }

        var queriesToCreate = newQueries.SelectMany(kvp => kvp.Value.SelectMany(x => ConvertToQuery(kvp.Key, x))).ToList();

        if (queriesToCreate.Count > 0) queryService.Create(queriesToCreate);

        return results;
    }

    private static List<Entities.Query> ConvertToQuery(string ShowTitle, string Season)
    {
        return new List<Entities.Query>
        {
            new Entities.Query()
            {
                Name= ShowTitle,
                QueryType = QueryType.Show,
                Filters =
                [
                    new()
                    {
                        Field = QueryAllowedFields.Show,
                        Operator = QueryAllowedOperators.Equals,
                        Value = ShowTitle
                    }
                ]
            },
            new Entities.Query()
            {
                Name = ShowTitle + " - Season " + Season,
                QueryType = QueryType.ShowAndSeason,
                Filters =
                [
                    new()
                    {
                        Field = QueryAllowedFields.Show,
                        Operator = QueryAllowedOperators.Equals,
                        Value = ShowTitle
                    },
                    new()
                    {
                        Field = QueryAllowedFields.SeasonNumber,
                        Operator = QueryAllowedOperators.Equals,
                        Value = Season
                    }
                ]
            }
        };
    }

    private record EpisodeMeta(int Season, int EpisodeStart, int? EpisodeEnd);
    private static EpisodeMeta? ExtractEpisodeMeta(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            return null;

        // SxxExx or SxxExx-Exx
        var sxeMatch = SxeMatch().Match(fileName);

        if (sxeMatch.Success)
        {
            int season = int.Parse(sxeMatch.Groups[1].Value);
            int epStart = int.Parse(sxeMatch.Groups[2].Value);

            int? epEnd = null;
            if (sxeMatch.Groups[3].Success)
                epEnd = int.Parse(sxeMatch.Groups[3].Value);

            return new EpisodeMeta(season, epStart, epEnd);
        }

        // Compact 3-digit format (101)
        var compactMatch = CompactMatch().Match(fileName);

        if (compactMatch.Success)
        {
            return new EpisodeMeta(
                int.Parse(compactMatch.Groups[1].Value),
                int.Parse(compactMatch.Groups[2].Value),
                null
            );
        }

        // Leading episode only (01 - Title)
        var leadingMatch = LeadingMatch().Match(fileName);

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

    [GeneratedRegex(@"\s\(.*\)$")]
    private static partial Regex ShowRegex();
    [GeneratedRegex(@"[^0-9]")]
    private static partial Regex SeasonNumber();
    [GeneratedRegex(@"\b(\d)(\d{2})\b")]
    private static partial Regex CompactMatch();
    [GeneratedRegex(@"^(\d{1,2})\s*-")]
    private static partial Regex LeadingMatch();
    [GeneratedRegex(@"\b[Ss](\d{1,2})[.\s_-]*[Ee](\d{1,2})(?:-(?:[Ee]?)?(\d{1,2}))?", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex SxeMatch();
}
