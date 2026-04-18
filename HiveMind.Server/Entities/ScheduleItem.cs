
namespace HiveMind.Server.Entities;

public class ScheduleItem
{
    public int ScheduleItemId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Index { get; set; }
    public string Type { get; set; } = string.Empty; // Block or Generic
    public ICollection<QueryScheduleItem>? Queries { get; set; }
    public int ScheduleId { get; set; }
    public Schedule? Schedule { get; set; }
}
