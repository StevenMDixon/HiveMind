namespace HiveMind.Server.Entities;

public class ProgramRotation
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Show {get; set;} = string.Empty;    
    public int? Season {get; set;}
    public DateOnly StartDate {get; set;}
    public RecurrenceType Recurrence {get; set;} 
    public int Interval {get; set;}
    public int? ProgramStrategyId {get; set;}    
    public ProgramStrategy? programStrategy {get; set;}
}

public enum RecurrenceType
{
    Daily,
    Weekly,
    Monthly
}
