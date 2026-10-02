using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Entities;
using HiveMind.Server.Services;

namespace HiveMind.Server.Domain.Scheduler.Nodes;

public class BlockNode: INode
{
    public List<INode> Slots { get; set; } = [];
    public string Name { get; set; } = string.Empty;
    public string Logo { get; set; } = string.Empty;

    public Dictionary<TransitionSlot, SourceItem> Sources { get; set; } = [];
    public Dictionary<TransitionType, int> Transitions { get; set; } = [];

    public IEnumerable<GenerationResultItem> Generate(GenerationContext context)
    {
        var blockContext = new BlockContext
        {
            Name = Name,
            Logo = Logo,
            Sources = Sources,
            Transitions = RetrieveTransitionSlots(context, Transitions),
            ProgramsNodeCount = Slots.Count
        };

        //Store this blocks context for later use in the generation process
        context.BlockContext.Add(blockContext);

        // Have the Block Context pull its opening and closing
        if (Sources.ContainsKey(TransitionSlot.BlockIntro))
        {
            var intro = context.Retriever.GetMedia(Sources[TransitionSlot.BlockIntro], RetreiverType.Random).First();
            blockContext.Intro = intro;
        }

        if (Sources.ContainsKey(TransitionSlot.BlockOutro))
        {
            var outro = context.Retriever.GetMedia(Sources[TransitionSlot.BlockOutro], RetreiverType.Random).First();
            blockContext.Outro = outro;
        }

        var results = new List<GenerationResultItem>();

        for (var i = 0; i < Slots.Count; i++)
        {
            if(i == 0 && blockContext.Intro != null)
            {
                results.Add(new GenerationResultItem
                {
                    MediaItem = blockContext.Intro,
                    Type = TransitionSlot.BlockIntro,
                    StartTime = 0,
                    EndTime = blockContext.Intro.Duration
                });
            }

            results.AddRange(Slots[i].Generate(context));

            if(i == Slots.Count - 1 && blockContext.Outro != null)
            {
                results.Add(new GenerationResultItem
                {
                    MediaItem = blockContext.Outro,
                    Type = TransitionSlot.BlockOutro,
                    StartTime = 0,
                    EndTime = blockContext.Outro.Duration
                });
            }
        }

        // Add this blocks promos to the overall context
        if(Sources.ContainsKey(TransitionSlot.Promo))
        {
            context.PromoQueries.Add(Sources[TransitionSlot.Promo]);
        }

        return results;
    }

    public Dictionary<TransitionType, List<TransitionTemplateSlot>> RetrieveTransitionSlots(GenerationContext context, Dictionary<TransitionType, int> transitions)
    {
        var transitionTemplateService = context.Scope.ServiceProvider.GetRequiredService<TransitionTemplateService>();

        var slots = new Dictionary<TransitionType, List<TransitionTemplateSlot>>();

        foreach (var key in transitions.Keys)
        {
            var transitionTemplate = transitions[key];

            var transistionType = key;

            var template = transitionTemplateService.GetTransitionTemplateByID(transitionTemplate);

            if (template != null && template.Slots.Count != 0)
            {
                slots.Add(transistionType, [.. template.Slots.OrderBy(s => s.Index)]);
            }
        }

        return slots;
    }
}
