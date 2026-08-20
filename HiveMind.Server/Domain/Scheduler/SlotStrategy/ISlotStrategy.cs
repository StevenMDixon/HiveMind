using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Domain.Scheduler.Nodes;
using HiveMind.Server.Entities;

namespace HiveMind.Server.Domain.Scheduler.SlotStrategy;

public interface ISlotStrategy
{
    public List<GenerationResultItem> Generate(GenerationContext context, int QueryId, string showName, int Duration, RetreiverType retreiverType);
}
