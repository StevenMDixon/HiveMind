using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Domain.Scheduler.Sources;
using HiveMind.Server.Entities;


namespace HiveMind.Server.Domain.Scheduler.SlotStrategy;

public class CutFillerStrategy : ISlotStrategy
{
    public List<GenerationResultItem> Generate(GenerationContext context, IDataSource source, List<string> tags, int duration)
    {
        var remainingDuration = duration;

        var results = new List<GenerationResultItem>();

        while(remainingDuration > 0)
        {
            var mediaItems = MediaItemRetriever.GetMedia(context, source, 1);

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
