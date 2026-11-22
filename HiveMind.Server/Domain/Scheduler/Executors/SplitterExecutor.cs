using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Domain.Scheduler.splitter;


namespace HiveMind.Server.Domain.Scheduler.Executors;

public class SplitterExecutor: IExecutor
{
    public SplitterStrategy SplitStrategy { get; set; }

    public int? SplitInterval { get; set; } = 1;

    public IEnumerable<GenerationResultItem> Execute(GenerationContext genContext, NodeContext context, List<GenerationResultItem> data)
    {
        var results = new List<GenerationResultItem>();
        
        results.AddRange(data.SelectMany(x => Split(x)));
        
        return results;
    }

    private List<GenerationResultItem> Split(GenerationResultItem data)
    {
        var results = new List<GenerationResultItem>();

        var splitter = SplitStrategyResolver.Resolve(SplitStrategy);

        results.AddRange(splitter.Generate(data, SplitInterval ?? 1));

        if (results.Count == 0) results.Add(data);

        return results;
    }
}