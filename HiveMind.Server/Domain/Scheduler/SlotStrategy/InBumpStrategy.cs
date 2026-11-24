using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Domain.Scheduler.Sources;
using HiveMind.Server.Entities;

namespace HiveMind.Server.Domain.Scheduler.SlotStrategy;

public class InBumpStrategy: ISlotStrategy
{
    public List<GenerationResultItem> Generate(GenerationContext context, IDataSource source, List<string> tags, int duration)
    { 
        var customFilter = new List<(string, string, string)>()
        {
            ("Duration", "LessThan", duration.ToString())
        };

        if(tags.Count > 0) customFilter.AddRange(tags.Select(x => ("Tag", "Contains", x)));

        var mediaItems = MediaItemRetriever.GetMedia(context, source, 1, customFilter);

        return [.. mediaItems.Select(x => new GenerationResultItem() { MediaItem = x, Type = TransitionSlot.InBump, StartTime = 0, EndTime = x.Duration })];
    }
}
