using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Domain.Scheduler.DataRetrievers;
using HiveMind.Server.Domain.Scheduler.Executors;
using HiveMind.Server.Domain.Scheduler.Sources;


namespace HiveMind.Server.Domain.Scheduler.Nodes;

public class BlockSegmentNode: INode
{
    public string Name { get; set; } = string.Empty;
    public string? Logo { get; set; } = string.Empty;
    public Dictionary<TransitionSlot, IDataSource>? Sources { get; set; }
    public Dictionary<TransitionType, int>? Transitions { get; set; }
    public required IDataRetriever Retriever { get; set; }
    public int Duration { get; set; }
    public bool UseParentName { get; set; }
    public SplitterExecutor? SplitConfig {get; set;}

    public IEnumerable<GenerationResultItem> Generate(GenerationContext context,  NodeContext? nodeContext)
    {
        var result = new List<GenerationResultItem>();

        var sContext = new NodeContext()
        {
            Duration = Duration * 60  * 1000,
            UsedDuration = nodeContext!.UsedDuration,
            Name = UseParentName ? nodeContext!.Name : Name,
            Sources = Utilities.ResolveSources(nodeContext?.Sources, this.Sources),
            Transitions = Transitions != null ? Utilities.RetrieveTransitionSlots(context, Transitions) : nodeContext!.Transitions
        };

        var customFilters = new List<(string, string, string)>
        {
            ("Tag", "Equals", $"{Name}")
        };

        if (UseParentName) customFilters.Add(("Tag", "Contains", $"{nodeContext!.Name}"));

        if (sContext.Sources.ContainsKey(TransitionSlot.ShowIntro))
        {
            sContext.Intro = MediaItemRetriever.GetMedia(context, sContext.Sources[TransitionSlot.ShowIntro], 1, customFilters).FirstOrDefault();
        }

        if (sContext.Sources.ContainsKey(TransitionSlot.ShowOutro))
        {
            sContext.Outro = MediaItemRetriever.GetMedia(context, sContext.Sources[TransitionSlot.ShowOutro], 1, customFilters).FirstOrDefault();
        }

        // Peel off show related durations from remaining duration
        sContext.UsedDuration += (sContext.Intro?.Duration ?? 0) + (sContext.Outro?.Duration ?? 0); 
        
        if (sContext.Intro != null) result.Add(new GenerationResultItem() { MediaItem = sContext.Intro, StartTime = 0, EndTime = sContext.Intro.Duration, Type = TransitionSlot.ShowIntro });

        var selectedItems = Retriever.Retrieve(context);

        if(SplitConfig != null) selectedItems = SplitConfig.Execute(context, sContext, selectedItems.ToList());

        var be = new BreakExecutor();
        result.AddRange(be.Execute(context, sContext, selectedItems.ToList()));

        if (sContext.Outro != null) result.Add(new GenerationResultItem() { MediaItem = sContext.Outro , StartTime = 0, EndTime = sContext.Outro.Duration, Type = TransitionSlot.ShowOutro });

        return result;
    }
}
