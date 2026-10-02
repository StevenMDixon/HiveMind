namespace HiveMind.Server.Domain.Scheduler.Nodes;

public class RepeatNode : INode
{
    public int RepeatCount { get; set; }

    public INode Slot { get; set; } = null!;
     
    public IEnumerable<GenerationResultItem> Generate(GenerationContext context)
    {
        var result = new List<GenerationResultItem>();
        
        for (int i = 0; i < RepeatCount; i++)
        {
            result.AddRange(Slot.Generate(context));
        }

        return result;
    }
}