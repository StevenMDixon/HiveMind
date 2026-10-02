using HiveMind.Server.Entities;

namespace HiveMind.Server.Domain.Scheduler;

public class ScheduleContext
{
    public DateOnly Date { get; set; }

    public int ProgramId { get; set; }
}
