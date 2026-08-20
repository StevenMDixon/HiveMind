using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Domain.Scheduler.Generators;
using HiveMind.Server.Entities;
using HiveMind.Server.Services;

namespace HiveMind.Server.Domain.Scheduler.Nodes;

public class BlockGenerator : IBlock
{
    public string Name { get; set; } = string.Empty;
    public string Logo { get; set; } = string.Empty;

    public Dictionary<TransitionSlot, int> Presentations { get; set; } = new Dictionary<TransitionSlot, int>();

    public Dictionary<TransitionType, int> Transitions { get; set; } = new Dictionary<TransitionType, int>();

    public List<ProgramGenerator> Programs { get; set; } = new List<ProgramGenerator>();

    public int Duration() => Programs.Sum(x => x.Duration);

    public bool UseBlockName { get; set; } = false;

    private Dictionary<TransitionType, List<TransitionTemplateSlot>> _BreakSlots = new Dictionary<TransitionType, List<TransitionTemplateSlot>>();

    public IEnumerable<GenerationResultItem> Generate(GenerationContext context)
    {
        var events = new List<GenerationResultItem>();

        var sectionedPrograms = new List<ProgramResults>();

        _BreakSlots = RetrieveTransitionSlots(context, Transitions);

        foreach (var program in Programs)
        {
            var slices = program.Generate(context);

            sectionedPrograms.Add(new ProgramResults(slices, slices.Sum(x => x.Duration()), program.Duration * 60 * 1000, program.UseBlockName ? Name : program.ShowName));
        }

        var currentBlockContext = context.BlockContext.Last();

        if(currentBlockContext != null) currentBlockContext.Shows.Add(Name);

        if (Presentations.ContainsKey(TransitionSlot.Promo)) {
            context.PromoQueries.Add((Presentations[TransitionSlot.Promo], Name));
        }

        for (var i = 0; i < sectionedPrograms.Count(); i++)
        {
            var currentProgram = sectionedPrograms[i];

            if(!UseBlockName)
            {
                if (currentBlockContext != null) currentBlockContext.Shows.Add(currentProgram.showName);

                if (Presentations.ContainsKey(TransitionSlot.Promo)) context.PromoQueries.Add(((Presentations[TransitionSlot.Promo], currentProgram.showName)));
            }

            var t = BuildSchedule(context, currentProgram, i == 0, i == sectionedPrograms.Count() - 1);
            events.AddRange(t);
        }

        return events;
    }

    private record ProgramResults(IEnumerable<GenerationResultItem> nodeSlices, int usedDuration, int TotalDuration, string showName);

    private List<GenerationResultItem> BuildSchedule(GenerationContext context, ProgramResults program, bool isFirst, bool isLast)
    {
        var remainingDuration = program.TotalDuration - program.usedDuration;

        var results = new List<GenerationResultItem>();

        int insertionIndex = 0;

        if(isFirst && Presentations.ContainsKey(TransitionSlot.BlockIntro))
        {
            var intro = context.Retriever.GetMedia(Presentations[TransitionSlot.BlockIntro], RetreiverType.Random).First();

            if(intro != null)
            {
                remainingDuration -= intro.Duration;
                results.Add(new GenerationResultItem { MediaItem = intro, StartTime = 0, EndTime = intro.Duration, Type = TransitionSlot.BlockIntro });
                insertionIndex += 1;
            }
        }

        if(isLast && Presentations.ContainsKey(TransitionSlot.BlockOutro))
        {
            var outro = context.Retriever.GetMedia(Presentations[TransitionSlot.BlockOutro], RetreiverType.Random).First();

            if(outro != null)
            {
                remainingDuration -= outro.Duration;
                results.Add(new GenerationResultItem { MediaItem = outro, StartTime = 0, EndTime = outro.Duration, Type = TransitionSlot.BlockOutro });
            }
        }

        if(Presentations.ContainsKey(TransitionSlot.ShowIntro))
        {
            var showIntro = context.Retriever.GetMedia(Presentations[TransitionSlot.ShowIntro], RetreiverType.Random, 1, new List<(string, string, string)>{("Show", "Contains", program.showName)}).First();

            if (showIntro != null)
            {
                remainingDuration -= showIntro.Duration;
                results.Add(new GenerationResultItem { MediaItem = showIntro, StartTime = 0, EndTime = showIntro.Duration, Type = TransitionSlot.ShowIntro });
                insertionIndex += 1;
            }
        }

        var breakBudget = remainingDuration / (program.nodeSlices.Count());

        for (var i = 0; i < program.nodeSlices.Count(); i ++)
        {
            var breakType = i == program.nodeSlices.Count() - 1 ? TransitionType.Postroll : TransitionType.Midroll;
           
            var breakResults = ResolveSlots(context, breakBudget, breakType);

            results.Add(program.nodeSlices.ElementAt(i));

            results.AddRange(breakResults);
        }

        return results;
    }

    public List<GenerationResultItem> ResolveSlots(GenerationContext context, int fillDuration, TransitionType type)
    {
        var results = new List<GenerationResultItem>();

        var breakSlots = _BreakSlots.ContainsKey(type) ? _BreakSlots[type] : new List<TransitionTemplateSlot>();

        if(breakSlots.Any())
        {
            results.AddRange(BreakGenerator.GenerateBreak(context, fillDuration, breakSlots, Presentations));
        }
        else
        {
            var fillerMedia = new Dictionary<TransitionSlot, int> { { TransitionSlot.Filler, Presentations.ContainsKey(TransitionSlot.Filler) ? Presentations[TransitionSlot.Filler] : 0 } };

            results.AddRange(BreakGenerator.GenerateBreak(context, fillDuration, new List<TransitionTemplateSlot> { new TransitionTemplateSlot() { Slot = TransitionSlot.Filler} }, fillerMedia));
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

            if (template != null && template.Slots.Any())
            {
                slots.Add(transistionType, template.Slots.ToList());
            }
        }

        return slots;
    }
}
