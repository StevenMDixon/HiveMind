using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Entities;


namespace HiveMind.Server.Domain.Scheduler.SlotStrategy;

public class CutFillerStrategy : ISlotStrategy
{
    public List<GenerationResultItem> Generate(GenerationContext context, SourceItem source, List<string> tags, int duration, RetreiverType retreiverType)
    {
        var retriever = context.Retriever;

        var remainingDuration = duration;

        var results = new List<GenerationResultItem>();

        while(remainingDuration > 0)
        {
            var mediaItems = retriever.GetMedia(source, retreiverType, 1);

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
