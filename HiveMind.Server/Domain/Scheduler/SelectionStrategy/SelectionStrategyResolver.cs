using HiveMind.Server.Endpoints.Enums;

namespace HiveMind.Server.Domain.Scheduler.SelectionStrategy;

public static class SelectionStrategyResolver
{
    public static ISelectionStrategy Resolve(SelectionPolicy strategyName)
    {
        return strategyName switch
        {
            SelectionPolicy.Fill => new FillStrategy(),
            SelectionPolicy.Single => new SingleStrategy(),
            _ => new SingleStrategy()
        };
    }
}
