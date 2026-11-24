namespace HiveMind.Server.Entities;

public class ProgramStrategy
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool Active { get; set; } = false;
    public int AdvancedDays { get; set; } = 0;
    public DateOnly? LastScheduleDate { get; set; } = null;
    public ICollection<ProgramStrategyLineup>? Lineups { get; set; } = null;
    public ICollection<ProgramEvent>? Events { get; set; }
    public ICollection<ProgramDatePart>? DateParts {get; set;}
    public ICollection<ProgramRotation>? Rotations {get; set;}
}
