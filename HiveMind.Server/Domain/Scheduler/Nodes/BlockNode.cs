using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Entities;
using HiveMind.Server.Services;

namespace HiveMind.Server.Domain.Scheduler.Nodes;

public class BlockNode: INode
{
    public string Name { get; set; } = string.Empty;
    public string? Logo { get; set; } = string.Empty;
    public Dictionary<TransitionSlot, SourceItem> Sources { get; set; } = [];
    public Dictionary<TransitionType, int> Transitions { get; set; } = [];
    public List<INode> Segments { get; set; } = [];

    public IEnumerable<GenerationResultItem> Generate(GenerationContext context, NodeContext? nodeContext = null)
    {
        var blockContext = new NodeContext
        {
            Name = Name,
            Logo = Logo ?? "",
            Sources = Sources,
            Transitions = Utilities.RetrieveTransitionSlots(context, Transitions),
        };

        // Have the Block Context pull its opening and closing
        if (Sources.ContainsKey(TransitionSlot.BlockIntro))
        {
            var intro = context.Retriever.GetMedia(Sources[TransitionSlot.BlockIntro], RetreiverType.Random).First();
            blockContext.usedDuration += intro.Duration;
            blockContext.Intro = intro;
        }

        if (Sources.ContainsKey(TransitionSlot.BlockOutro))
        {
            var outro = context.Retriever.GetMedia(Sources[TransitionSlot.BlockOutro], RetreiverType.Random).First();
            blockContext.usedDuration += outro.Duration;
            blockContext.Outro = outro;
        }

        var results = new List<GenerationResultItem>();

        if(blockContext.Intro != null)
        {
            results.Add(new GenerationResultItem
            {
                MediaItem = blockContext.Intro,
                Type = TransitionSlot.BlockIntro,
                StartTime = 0,
                EndTime = blockContext.Intro.Duration
            });
        }

        foreach(var segment in Segments)
        {
            results.AddRange(segment.Generate(context, blockContext));
        }

        if(blockContext.Outro != null)
        {
            results.Add(new GenerationResultItem
            {
                MediaItem = blockContext.Outro,
                Type = TransitionSlot.BlockOutro,
                StartTime = 0,
                EndTime = blockContext.Outro.Duration
            });
        }

        // Add this blocks promos to the overall context
        if(Sources.ContainsKey(TransitionSlot.Promo))
        {
            context.PromoQueries.Add(Sources[TransitionSlot.Promo]);
        }

        return results;
    }
}
