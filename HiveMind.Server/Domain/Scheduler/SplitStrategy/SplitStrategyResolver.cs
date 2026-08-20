using HiveMind.Server.Domain.Enums;

namespace HiveMind.Server.Domain.Scheduler.splitter;

public static class SplitStrategyResolver
{
    public static ISplitStrategy Resolve(SplitterStrategy strategyName)
    {
        return strategyName switch
        {
            SplitterStrategy.FixedCount => new SplitFixedCount(),
            SplitterStrategy.Every => new SplitEvery(),
            _ => throw new ArgumentException($"Unknown strategy: {strategyName}")
        };
    }
}
