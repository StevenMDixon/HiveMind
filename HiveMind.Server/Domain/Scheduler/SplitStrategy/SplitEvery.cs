namespace HiveMind.Server.Domain.Scheduler.splitter;

public class SplitEvery: ISplitStrategy
{
    public List<GenerationResultItem> Generate(GenerationResultItem node, int interval)
    {
        var duration = node.Duration();

        var results = new List<GenerationResultItem>();

        var intervalMS = interval * 60 * 1000;

        var current = 0;

        while (current < duration)
        {
            results.Add(new GenerationResultItem() { MediaItem = node.MediaItem, StartTime = current, EndTime = current + intervalMS });
            current += intervalMS;
        }

        return results;
    }
}
