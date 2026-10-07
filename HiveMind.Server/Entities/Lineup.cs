namespace HiveMind.Server.Entities;

public class Lineup
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public TimeOnly StartTime { get; set; } = TimeOnly.MinValue;
    public string JsonData { get; set; } = string.Empty;
}
