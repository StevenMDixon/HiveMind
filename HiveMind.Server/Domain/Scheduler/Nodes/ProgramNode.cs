using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Entities;

namespace HiveMind.Server.Domain.Scheduler.Nodes;

public class ProgramNode: INode
{
    public INode? Slot { get; set; }
    public int Duration { get; set; }
    public bool UseBlockName { get; set; }
    public string? ProgramName { get; set; }

    public IEnumerable<GenerationResultItem> Generate(GenerationContext context)
    {
        var result = new List<GenerationResultItem>();

        var bContext = context.BlockContext.Last();

        var usedDurationFromBlock = ((bContext.Intro?.Duration ?? 0) + (bContext.Outro?.Duration ?? 0)) / (bContext.ProgramsNodeCount > 0 ? bContext.ProgramsNodeCount : 1);

        var pContext = new ProgramContext()
        {
            Duration = Duration * 60  * 1000,
            RemainingDuration = (Duration * 60 * 1000) - usedDurationFromBlock,
            ProgramName = ProgramName ?? string.Empty,
            UseBlockName = UseBlockName
        };

        bContext.ProgramContexts.Add(pContext);

        MediaItem? intro = null;
        MediaItem? outro = null;

        // Have program go ahead and pull the program intro:
        // @Todo: These need to be updated to respect blockName
        var customFilters = new List<(string, string, string)>
        {
            ("Tag", "Equals", $"{ProgramName}")
        };

        if (UseBlockName) customFilters.Add(("Tag", "Contains", $"{bContext.Name}"));

        if (bContext.Sources.ContainsKey(TransitionSlot.ShowIntro))
        {
            intro = context.Retriever.GetMedia(bContext.Sources[TransitionSlot.ShowIntro], RetreiverType.Random, 1, customFilters).FirstOrDefault();
        }

        if (bContext.Sources.ContainsKey(TransitionSlot.ShowOutro))
        {
            outro = context.Retriever.GetMedia(bContext.Sources[TransitionSlot.ShowOutro], RetreiverType.Random, 1, customFilters).FirstOrDefault();
        }

        // Peel off show related durations from remaining duration
        pContext.RemainingDuration -= (intro?.Duration ?? 0) + (outro?.Duration ?? 0); 

        if (Slot != null)
        {
            if (intro != null) result.Add(new GenerationResultItem() { MediaItem = intro, StartTime = 0, EndTime = intro.Duration, Type = TransitionSlot.ShowIntro });

            result.AddRange(Slot.Generate(context));

            if (outro != null) result.Add(new GenerationResultItem() { MediaItem = outro, StartTime = 0, EndTime = outro.Duration, Type = TransitionSlot.ShowOutro });
        }

        return result;
    }
}
