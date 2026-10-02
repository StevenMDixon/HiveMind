using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Domain.Scheduler.splitter;

namespace HiveMind.Server.Domain.Scheduler.Nodes;

public class SplitterNode: INode
{
    public INode? Slot { get; set; }

    public SplitterStrategy SplitStrategy { get; set; }

    public int SplitInterval { get; set; } = 1;

    public IEnumerable<GenerationResultItem> Generate(GenerationContext context)
    {
        var results = new List<GenerationResultItem>();

        if(Slot != null)
        {
            var selectedMedia = Slot.Generate(context);

            if(selectedMedia != null) results.AddRange(selectedMedia.SelectMany(x => Split(x)));
        }
        
        return results;
    }

    private List<GenerationResultItem> Split(GenerationResultItem node)
    {
        var results = new List<GenerationResultItem>();

        var splitter = SplitStrategyResolver.Resolve(SplitStrategy);

        results.AddRange(splitter.Generate(node, SplitInterval));

        if (results.Count == 0) results.Add(node);

        return results;
    }
}
