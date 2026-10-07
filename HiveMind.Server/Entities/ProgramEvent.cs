using System.ComponentModel.DataAnnotations;

namespace HiveMind.Server.Entities;

public class ProgramEvent
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int? ProgramStrategyId { get; set; }
    public ProgramStrategy? ProgramStrategy { get; set; }
    public int PromoDays { get; set; } = 1;
    public DateTime EventDate { get; set; }
    public int QueryId { get; set; }
    public Query Query { get; set; } = null!;
}
