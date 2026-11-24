using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Domain.Scheduler.Sources;

namespace HiveMind.Server.Domain.Scheduler.SlotStrategy;

public interface ISlotStrategy
{
    public List<GenerationResultItem> Generate(GenerationContext context, IDataSource source, List<string> tags, int duration);
}
