using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Entities;

namespace HiveMind.Server.Domain.Scheduler.Nodes;

public class GenerationResultItem()
{
    public MediaItem MediaItem { get; set; } = null!;

    public TransitionSlot Type { get; set; } = TransitionSlot.Media;

    public int StartTime { get; set; }

    public int EndTime { get; set; }

    public int Duration() => EndTime - StartTime;
}