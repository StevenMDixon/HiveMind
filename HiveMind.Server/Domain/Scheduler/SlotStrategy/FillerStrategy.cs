using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Entities;
using static HiveMind.Server.Domain.Enums.QueryEnums;


namespace HiveMind.Server.Domain.Scheduler.SlotStrategy;

public class FillerStrategy : ISlotStrategy
{
    public List<GenerationResultItem> Generate(GenerationContext context, SourceItem source, List<string> tags, int duration, RetreiverType retreiverType)
    {
        var retriever = context.Retriever;

        var remainingDuration = duration;

        var results = new List<GenerationResultItem>();

        while(remainingDuration > 0)
        {
            var customFilter = new List<(string, string, string)>()
            {
                (QueryAllowedFields.Duration.ToString(), QueryAllowedOperators.LessThanEquals.ToString(), duration.ToString()),
            };

            var mediaItems = retriever.GetMedia(source, retreiverType, 1, customFilter);

            if(mediaItems.Count != 0)
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
