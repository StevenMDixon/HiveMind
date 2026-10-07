using HiveMind.Server.Domain.Enums;

namespace HiveMind.Server.Entities;

public class ProgramStrategyLineup
{
    public int Id { get; set; }

    public int? ProgramStrategyId { get; set; } = null;

    public ProgramStrategy? ProgramStrategy { get; set; } = null;

    public int? LineupId { get; set; } = null;

    public Lineup? Lineup { get; set; } = null;

    public LineupSelectionType SelectionType { get; set; } = 0; // Any, Day, Date, Month,

    public string SelectionOption { get; set; } = ""; // Any, "" 
}
