using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Domain.Scheduler.Nodes;
using HiveMind.Server.Entities;

namespace HiveMind.Server.Domain.Scheduler.SlotStrategy;

public class InBumpStrategy: ISlotStrategy
{
    public List<GenerationResultItem> Generate(GenerationContext context, int QueryId, string showName, int Duration, RetreiverType retreiverType)
    { 
    var retriever = context.Retriever;

    var customFilter = new List<(string, string, string)>()
        {
            ("Duration", "LessThan", Duration.ToString())
        };

    var mediaItems = retriever.GetMedia(QueryId, retreiverType, 1, customFilter);

    return mediaItems.Select(x => new GenerationResultItem() { MediaItem = x, Type = TransitionSlot.InBump, StartTime = 0, EndTime = x.Duration }).ToList();
    }
}
