using HiveMind.Server.Domain.Enums;

namespace HiveMind.Server.Domain.Scheduler.SlotStrategy;

public interface ISlotStrategy
{
    public List<GenerationResultItem> Generate(GenerationContext context, SourceItem source, List<string> tags, int duration, RetreiverType retreiverType);
}
