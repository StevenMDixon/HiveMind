using System.Numerics;

namespace HiveMind.Server.Entities;

public class ScheduleItem
{
    public int ScheduleItemId { get; set; }
    public int Index { get; set; }
    public string Type { get; set; } = string.Empty; // Block or Generic
    public ICollection<CollectionScheduleItem>? Collections { get; set; }
    public int ScheduleId { get; set; }
    public Schedule? Schedule { get; set; }
}
