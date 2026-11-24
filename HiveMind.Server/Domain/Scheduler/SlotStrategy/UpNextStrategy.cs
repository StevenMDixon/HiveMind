using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Domain.Scheduler.SlotStrategy;
using HiveMind.Server.Domain.Scheduler.Sources;
using HiveMind.Server.Entities;
using static HiveMind.Server.Domain.Enums.QueryEnums;

namespace HiveMind.Server.Domain.Scheduler.SlotStrategy;

public class UpNextStrategy: ISlotStrategy
{
    public List<GenerationResultItem> Generate(GenerationContext context, IDataSource source, List<string> tags, int duration)
    {
        var results = new List<MediaItem>();

        var customFilter = new List<(string, string, string)>()
        {
            (QueryAllowedFields.Duration.ToString(), QueryAllowedOperators.LessThanEquals.ToString(), duration.ToString()),
        };

        var mediaItems = MediaItemRetriever.GetMedia(context, source, 1, customFilter);

        return [.. mediaItems.Select(x => new GenerationResultItem() { MediaItem = x, Type = TransitionSlot.UpNext, StartTime = 0, EndTime = x.Duration })];
    }
}
