namespace HiveMind.Server.Domain.Scheduler.Nodes;

using HiveMind.Server.Domain.Scheduler;
using HiveMind.Server.Entities;

public class MediaServerNode: INode
{
    public string URL { get; set; } = null!;
    public int Duration { get; set; } = 0;
    public IEnumerable<GenerationResultItem> Generate(GenerationContext context)
    {
        var mediaItem = new MediaItem()
        {
            FilePath = URL,
            Duration = Duration * 60 * 1000,
        };

        return [.. new List<GenerationResultItem>() { new() {
            MediaItem = mediaItem,
            StartTime = 0,
            EndTime = mediaItem.Duration,
        } }];
    }
}
