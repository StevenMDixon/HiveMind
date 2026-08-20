using HiveMind.Server.Domain.Scheduler.Nodes;

namespace HiveMind.Server.Domain.Scheduler.splitter;

public class SplitEvery: ISplitStrategy
{
    public List<GenerationResultItem> Generate(GenerationResultItem node, int interval)
    {
        var duration = node.Duration();

        var results = new List<GenerationResultItem>();

        var intervalMS = interval * 60 * 1000;

        var current = intervalMS;

        while (current < duration)
        {
            results.Add(new GenerationResultItem() { MediaItem = node.MediaItem, StartTime = intervalMS, EndTime = current + intervalMS });
            current += intervalMS;
        }

        return results;
    }
}
