using HiveMind.Server.Domain.Enums;

namespace HiveMind.Server.Domain.Scheduler.DataRetrievers;

public class QueryRetreiver: IDataRetriever
{
    public SourceItem Query { get; set; } = null!;
    public RetreiverType RetrievalSetting { get; set; } = RetreiverType.Random;
    public IEnumerable<GenerationResultItem> Retrieve(GenerationContext context)
    {
        var results = new List<GenerationResultItem>();

        var media = context.Retriever.GetMedia(Query, RetrievalSetting).FirstOrDefault();

        if (media != null) results.Add(new GenerationResultItem { MediaItem = media, StartTime = 0, EndTime = media.Duration });

        return results;
    }
}