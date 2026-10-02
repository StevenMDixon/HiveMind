namespace HiveMind.Server.Entities;

public class Lineup
{
    public int LineupId { get; set; }
    public string LineupName { get; set; } = string.Empty;
    public TimeOnly StartTime { get; set; } = TimeOnly.MinValue;
    public string JsonData { get; set; } = string.Empty;
}
