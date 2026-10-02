namespace HiveMind.Server.Domain.Scheduler.splitter;

public class SplitFixedCount: ISplitStrategy
{
    public List<GenerationResultItem> Generate(GenerationResultItem node, int interval)
    {
        var duration = node.Duration();

        var results = new List<GenerationResultItem>();

        var partDuration = duration / interval;

        for (int i = 0; i < interval; i++)
        {
            results.Add(new GenerationResultItem() { MediaItem = node.MediaItem, StartTime = partDuration * i, EndTime = partDuration * (i + 1) });
        }

        return results;
    }
}
