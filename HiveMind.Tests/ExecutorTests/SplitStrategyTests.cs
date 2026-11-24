using System.ComponentModel.Design;
using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Domain.Scheduler;
using HiveMind.Server.Domain.Scheduler.splitter;
using HiveMind.Server.Entities;

namespace HiveMind.Server.Tests.SplitStrategyTests;

public class SplitStrategyTests
{
    [Fact]
    public void EveryReturnsExpectedNumberOfSplits()
    {
        var testMediaItem = new GenerationResultItem(){StartTime = 0, EndTime = 10 * 60 * 1000};

        var resolver = SplitStrategyResolver.Resolve(SplitterStrategy.Every);

        var result = resolver.Generate(testMediaItem, 1);

        Assert.Equal(10, result.Count);
    }

    [Fact]
    public void FixedReturnsExpectedNumberOfSplits()
    {
        var testMediaItem = new GenerationResultItem(){StartTime = 0, EndTime = 10 * 60 * 1000};

        var resolver = SplitStrategyResolver.Resolve(SplitterStrategy.FixedCount);

        var result = resolver.Generate(testMediaItem, 5);

        Assert.Equal(5, result.Count);
    }
}