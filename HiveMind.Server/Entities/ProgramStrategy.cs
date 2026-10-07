

namespace HiveMind.Server.Entities;

public class ProgramStrategy
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    // Does the scheduler need to create a schedule for this item?
    public bool Active { get; set; } = false;

    // How many days in the future to schedule.
    public int AdvancedDays { get; set; } = 0;

    public DateOnly? LastScheduleDate { get; set; } = null;

    public ICollection<ProgramStrategyLineup>? Lineups { get; set; } = null;

}
