using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Domain.Scheduler.Sources;
using HiveMind.Server.Entities;

namespace HiveMind.Server.Domain.Scheduler.SlotStrategy;

public class CommercialStrategy: ISlotStrategy
{
    public List<GenerationResultItem> Generate(GenerationContext context, IDataSource source, List<string> tags, int duration)
    {
        var customFilter = new List<(string, string, string)>()
        {
            ("Duration", "LessThan", duration.ToString())
        };

        var mediaItems = MediaItemRetriever.GetMedia(context, source, 1, customFilter);

        return [.. mediaItems.Select(x => new GenerationResultItem() { MediaItem = x, Type = TransitionSlot.Commercial, StartTime = 0, EndTime = x.Duration })];
    }
}
