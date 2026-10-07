namespace HiveMind.Server.Entities;

public class ScheduleBatchItem
{
    public int Id { get; set; }
    public DateOnly ScheduleDate { get; set; }
    public bool IsCompleted { get; set; } = false;
    public string OutputFilePath { get; set; } = string.Empty;
    public int ScheduleBatchId { get; set; }
    public int? ProgramStrategyLineupId { get; set; }
}
