namespace HiveMind.Server.Entities;

public class Lineup
{
    public int LineupId { get; set; }
    public string LineupName { get; set; } = string.Empty;
    public TimeOnly StartTime { get; set; }
    public ICollection<LineupItem>? LineupItems { get; set; }
}
