using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Domain.Scheduler.Nodes;
using HiveMind.Server.Domain.Scheduler.SlotStrategy;
using HiveMind.Server.Entities;
using static HiveMind.Server.Domain.Enums.QueryEnums;

namespace HiveMind.Server.Domain.Scheduler.SlotStrategy;

public class UpNextStrategy: ISlotStrategy
{
    public List<GenerationResultItem> Generate(GenerationContext context, int QueryId, string showName, int Duration, RetreiverType retreiverType)
    {
        var results = new List<MediaItem>();

        var retriever = context.Retriever;

        var customFilter = new List<(string, string, string)>()
        {
            (QueryAllowedFields.Duration.ToString(), QueryAllowedOperators.LessThanEquals.ToString(), Duration.ToString()),
            (QueryAllowedFields.Title.ToString(), QueryAllowedOperators.Contains.ToString(), showName)
        };

        var mediaItems = retriever.GetMedia(QueryId, retreiverType, 1, customFilter);

        return mediaItems.Select(x => new GenerationResultItem() { MediaItem = x, Type = TransitionSlot.UpNext, StartTime = 0, EndTime = x.Duration }).ToList();
    }
}
