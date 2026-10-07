using HiveMind.Server.Domain.Enums;

namespace HiveMind.Server.Entities;

public class ScheduleBatch
{
    public int Id { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public BatchStatus Status { get; set; } = BatchStatus.New;

    public int? ProgramStrategyId { get; set; } = null;

    public ProgramStrategy? ProgramStrategy { get; set; } = null!;

    public ICollection<ScheduleBatchItem>? ScheduleBatchItems { get; set; } = null;
}
