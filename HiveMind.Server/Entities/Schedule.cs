namespace HiveMind.Server.Entities;

public class Schedule
{
    public int ScheduleId { get; set; }
    public string ScheduleName { get; set; } = string.Empty;
    public int ChannelId { get; set; }
    public TimeOnly StartTime { get; set; }
    public Channel? Channel { get; set; }
    public ICollection<ScheduleItem>? ScheduleItems { get; set; }
}
