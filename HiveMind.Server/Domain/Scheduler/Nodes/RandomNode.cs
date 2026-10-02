namespace HiveMind.Server.Domain.Scheduler.Nodes;

public class RandomNode: INode
{
    public List<INode> Slots { get; set; } = null!;
    public IEnumerable<GenerationResultItem> Generate(GenerationContext context)
    {
        var results = new List<GenerationResultItem>();

        var random = Random.Shared.Next(Slots.Count);

        results.AddRange(Slots[random].Generate(context));

        return results;
    }
}
