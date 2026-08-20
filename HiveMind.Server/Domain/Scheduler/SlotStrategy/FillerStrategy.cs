using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Domain.Scheduler.Nodes;
using HiveMind.Server.Entities;
using static HiveMind.Server.Domain.Enums.QueryEnums;


namespace HiveMind.Server.Domain.Scheduler.SlotStrategy;

public class FillerStrategy : ISlotStrategy
{
    public List<GenerationResultItem> Generate(GenerationContext context, int QueryId, string showName, int Duration, RetreiverType retreiverType)
    {
        var retriever = context.Retriever;

        var remainingDuration = Duration;

        var results = new List<GenerationResultItem>();

        while(remainingDuration > 0)
        {
            var customFilter = new List<(string, string, string)>()
            {
                (QueryAllowedFields.Duration.ToString(), QueryAllowedOperators.LessThanEquals.ToString(), Duration.ToString()),
            };

            var mediaItems = retriever.GetMedia(QueryId, retreiverType, 1, customFilter);

            if(mediaItems.Any())
            {
                var selectedMedia = mediaItems.First();
                remainingDuration -= selectedMedia.Duration;
                results.Add(new GenerationResultItem() { MediaItem = selectedMedia, Type = TransitionSlot.Filler, StartTime = 0, EndTime = selectedMedia.Duration + (remainingDuration < 0 ? remainingDuration : 0)});
            } 
            else
            {
                break;
            }
        }

        return results;
    }
}
