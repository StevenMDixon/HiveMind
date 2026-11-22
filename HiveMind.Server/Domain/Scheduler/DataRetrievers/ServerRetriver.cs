using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Domain.Scheduler;
using HiveMind.Server.Entities;

namespace HiveMind.Server.Domain.Scheduler.DataRetrievers;

public class ServerRetreiver: IDataRetriever
{
    public string URL { get; set; } = null!;
    public int Duration { get; set; } = 0;
    public IEnumerable<GenerationResultItem> Retrieve(GenerationContext context)
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