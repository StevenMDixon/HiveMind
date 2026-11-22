using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Domain.Scheduler;
using HiveMind.Server.Entities;
using HiveMind.Server.Services;

namespace HiveMind.Server.Domain.Scheduler.Nodes;
public static class Utilities
{
    public static Dictionary<TransitionType, List<TransitionTemplateSlot>> RetrieveTransitionSlots(GenerationContext context, Dictionary<TransitionType, int> transitions)
    {
        var transitionTemplateService = context.ServiceProvider.GetRequiredService<TransitionTemplateService>();

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

    public static Dictionary<TransitionSlot, SourceItem> ResolveSources(Dictionary<TransitionSlot, SourceItem>? baseSources, Dictionary<TransitionSlot, SourceItem>? applyingSources)
    {
        var newSources = new Dictionary<TransitionSlot, SourceItem>();

        if(baseSources != null) baseSources.Keys.ToList().ForEach(x =>  newSources.Add(x, baseSources[x]));

        if(applyingSources != null) applyingSources.Keys.ToList().ForEach(x =>  newSources.Add(x, applyingSources[x]));

        return newSources;
    }
}