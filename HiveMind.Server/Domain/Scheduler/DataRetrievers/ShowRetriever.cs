using HiveMind.Server.Domain.Enums;

namespace HiveMind.Server.Domain.Scheduler.DataRetrievers;

public class ShowRetreiver: IDataRetriever
{
    public string ShowName { get; set; } = null!;

    public int? Season { get; set; }

    public int? Episode { get; set; }

    public IEnumerable<GenerationResultItem> Retrieve(GenerationContext context)
    {
        var results = new List<GenerationResultItem>();
        var queries = new List<(string, string, string)>();

        queries.Add(("Show", "Equals", ShowName));

        if(Season.HasValue) queries.Add(("SeasonNumber", "Equals", Season.Value.ToString()));
        if(Episode.HasValue) queries.Add(("EpisodeNumber", "Equals", Episode.Value.ToString()));

        var possibleMedia = context.ScheduleDataProvider.Queries.GetMediaItemsByQueries(queries);

        var media =  possibleMedia.OrderBy(r => Guid.NewGuid()).FirstOrDefault();

        if (media != null) results.Add(new GenerationResultItem { MediaItem = media, StartTime = 0, EndTime = media.Duration });

        return results;
    }
}