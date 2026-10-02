using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Entities;
using static HiveMind.Server.Domain.Enums.QueryEnums;

namespace HiveMind.Server.Domain.Scheduler.SlotStrategy;

public class BumpStrategy : ISlotStrategy
{
    public List<GenerationResultItem> Generate(GenerationContext context, SourceItem source, string showName, int Duration, RetreiverType retreiverType)
    {
        var retriever = context.Retriever;

        var customFilter = new List<(string, string, string)>()
        {
            (QueryAllowedFields.Duration.ToString(), QueryAllowedOperators.LessThanEquals.ToString(), Duration.ToString())
        };

        var mediaItems = retriever.GetMedia(source, retreiverType, 1, customFilter);

        return mediaItems.Select(x => new GenerationResultItem() { MediaItem = x , Type = TransitionSlot.Bump, StartTime = 0, EndTime = x.Duration}).ToList();
    }
}
