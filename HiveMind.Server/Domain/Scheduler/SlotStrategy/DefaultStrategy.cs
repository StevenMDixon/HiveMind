using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Entities;

namespace HiveMind.Server.Domain.Scheduler.SlotStrategy;

public class DefaultStrategy: ISlotStrategy
{
    public List<GenerationResultItem> Generate(GenerationContext context, SourceItem source, List<string> tags, int duration, RetreiverType retreiverType)
    {
        var retriever = context.Retriever;

        var customFilter = new List<(string, string, string)>()
        {
            ("Duration", "LessThan", duration.ToString())
        };
        
        if(tags.Count > 0) customFilter.AddRange(tags.Select(x => ("Title", "Contains", x)));

        var mediaItems = retriever.GetMedia(source, retreiverType, 1, customFilter);

        return [.. mediaItems.Select(x => new GenerationResultItem() { MediaItem = x, Type = TransitionSlot.Media, StartTime = 0, EndTime = x.Duration })];
    }
}
