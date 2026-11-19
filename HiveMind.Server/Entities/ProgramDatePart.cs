using System.ComponentModel.DataAnnotations;

namespace HiveMind.Server.Entities;

public class ProgramDatePart
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public TimeOnly StartTime {get; set;}
    public TimeOnly EndTime {get; set;}
    public int? ProgramStrategyId {get; set;} = null!;
}
