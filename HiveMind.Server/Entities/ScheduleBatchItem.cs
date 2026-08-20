namespace HiveMind.Server.Entities;

public class ScheduleBatchItem
{
    public int ScheduleBatchItemId { get; set; }
    public DateOnly ScheduleDate { get; set; }
    public bool IsCompleted { get; set; } = false;
    public string OutputFilePath { get; set; } = string.Empty;
    public int? ScheduleBatchId { get; set; }
    public int? ProgramStrategyLineUpId { get; set; }
}
