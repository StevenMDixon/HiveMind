using HiveMind.Server.Domain.Scheduler.Nodes;

namespace HiveMind.Server.Domain.Scheduler.splitter;

public interface ISplitStrategy
{
    public List<GenerationResultItem> Generate(GenerationResultItem node, int interval);
}
