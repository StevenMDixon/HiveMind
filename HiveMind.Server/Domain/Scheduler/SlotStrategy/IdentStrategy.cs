using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Entities;

namespace HiveMind.Server.Domain.Scheduler.SlotStrategy;

public class IdentStrategy: ISlotStrategy
{
    public List<GenerationResultItem> Generate(GenerationContext context, SourceItem source, string showName, int Duration, RetreiverType retreiverType)
    {
        var retriever = context.Retriever;

        var customFilter = new List<(string, string, string)>()
        {
            ("Duration", "LessThan", Duration.ToString())
        };

        var mediaItems = retriever.GetMedia(source, retreiverType, 1, customFilter);

        return [.. mediaItems.Select(x => new GenerationResultItem() { MediaItem = x, Type = TransitionSlot.Ident, StartTime = 0, EndTime = x.Duration })];
    }
}
