using HiveMind.Server.Domain.Enums;

namespace HiveMind.Server.Domain.Scheduler.Nodes;

public class MediaShowNode: INode
{
    public string ShowName { get; set; } = null!;

    public int? Season { get; set; }

    public int? Episode { get; set; }

    public RetreiverType RetrievalSetting { get; set; } = RetreiverType.Random;

    public IEnumerable<GenerationResultItem> Generate(GenerationContext context)
    {
        var results = new List<GenerationResultItem>();
        var queries = new List<String>();
            
        queries.Insert(0, "Show, Equals, " + ShowName);

        if(Season.HasValue) queries.Insert(0, "SeasonNumber, Equals, " + Season.Value);
        if(Episode.HasValue) queries.Insert(0, "EpisodeNumber, Equals, " + Episode.Value);

        var media =  context.Retriever.GetMedia(queries.ToArray(), RetrievalSetting).FirstOrDefault();

        if (media != null) results.Add(new GenerationResultItem { MediaItem = media, StartTime = 0, EndTime = media.Duration });

        return results;
    }
}
