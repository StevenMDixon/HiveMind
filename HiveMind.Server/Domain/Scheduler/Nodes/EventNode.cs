using HiveMind.Server.Domain.Enums;

namespace HiveMind.Server.Domain.Scheduler.Nodes;

public class EventNode: INode
{
    public List<INode> Slots { get; set; } = new List<INode>();
    public string Name { get; set; } = string.Empty;
    public string Logo { get; set; } = string.Empty;

    public Dictionary<TransitionSlot, SourceItem> Sources { get; set; } = new Dictionary<TransitionSlot, SourceItem>();
    public Dictionary<TransitionType, int> Transitions { get; set; } = new Dictionary<TransitionType, int>();
    public IEnumerable<GenerationResultItem> Generate(GenerationContext context)
    {
        throw new NotImplementedException();
    }
}
