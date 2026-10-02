using HiveMind.Server.Domain.Enums;

namespace HiveMind.Server.Domain.Scheduler.Nodes;

public class EventNode: INode
{
    public List<INode> Slots { get; set; } = [];
    public string Name { get; set; } = string.Empty;
    public string Logo { get; set; } = string.Empty;

    public Dictionary<TransitionSlot, SourceItem> Sources { get; set; } = [];
    public Dictionary<TransitionType, int> Transitions { get; set; } = [];
    public IEnumerable<GenerationResultItem> Generate(GenerationContext context)
    {
        throw new NotImplementedException();
    }
}
