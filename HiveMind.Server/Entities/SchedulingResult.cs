namespace HiveMind.Server.Entities;

public class SchedulingResult
{
    public int Id { get; set; }
    public string Path { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public int? ProgramStrategyId { get; set; }
}
