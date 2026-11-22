using HiveMind.Server.Domain.Enums;

namespace HiveMind.Server.Domain.Scheduler.Executors;

public interface IExecutor
{
    public abstract IEnumerable<GenerationResultItem> Execute(GenerationContext genContext, NodeContext context, List<GenerationResultItem> data); 
}