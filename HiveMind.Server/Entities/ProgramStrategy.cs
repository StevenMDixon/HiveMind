

namespace HiveMind.Server.Entities;

public class ProgramStrategy
{
    public int ProgramStrategyId { get; set; }

    public string Name { get; set; } = string.Empty;

    // Does the scheduler need to create a schedule for this item?
    public bool Active { get; set; } = false;

    // How many days in the future to schedule.
    public int AdvancedDays { get; set; } = 1;

    public DateOnly? StartDate { get; set; } = new DateOnly();

    public DateOnly? EndDate { get; set; } = new DateOnly();

    public ICollection<ProgramStrategyLineup>? Lineups { get; set; } = null;

}
