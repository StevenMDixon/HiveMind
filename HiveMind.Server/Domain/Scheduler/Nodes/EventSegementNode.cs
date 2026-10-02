using HiveMind.Server.Domain.Enums;

namespace HiveMind.Server.Domain.Scheduler.Nodes;

public class EventSegementNode: INode
{
    public INode Slot { get; set; } = null!;

    public int Duration { get; set; }

    public Dictionary<TransitionSlot, SourceItem> Sources { get; set; } = new Dictionary<TransitionSlot, SourceItem>();

    public IEnumerable<GenerationResultItem> Generate(GenerationContext context)
    {
        var results = new List<GenerationResultItem>();

        var currentBlockContext = context.BlockContext.Last();

        var eventBlockContext = new BlockContext()
        {
            Name = currentBlockContext.Name,
            Logo = currentBlockContext.Logo,
            Sources = Sources,
            Transitions = currentBlockContext.Transitions,
            ProgramsNodeCount = 1
        };

        // Add a temp block context that overrides sources with the event sources, so that any child nodes will use these sources instead of the parent block context sources.
        context.BlockContext.Add(eventBlockContext);

        results.AddRange(Slot.Generate(context));

        // Remove the temp block context so that any child nodes will use the parent block context sources instead of the event sources.
        context.BlockContext.Remove(eventBlockContext);

        return results;
    }
}
