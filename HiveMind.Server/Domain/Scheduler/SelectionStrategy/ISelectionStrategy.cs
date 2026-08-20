using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Domain.Scheduler.Nodes;
using HiveMind.Server.Entities;

namespace HiveMind.Server.Domain.Scheduler.SelectionStrategy;

public interface ISelectionStrategy
{
    public List<MediaItem> Generate(GenerationContext context, List<string> Queries, int Duration, RetreiverType retreiverType, string ShowName);
}
