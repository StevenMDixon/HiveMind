using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Domain.Scheduler.Sources;
using HiveMind.Server.Entities;

namespace HiveMind.Server.Domain.Scheduler.Nodes;
public static class Utilities
{
    public static Dictionary<TransitionType, List<TransitionTemplateSlot>> RetrieveTransitionSlots(GenerationContext context, Dictionary<TransitionType, int> transitions)
    {
        var transitionTemplateService = context.ScheduleDataProvider.TransitionTemplates;

        var slots = new Dictionary<TransitionType, List<TransitionTemplateSlot>>();

        foreach (var key in transitions.Keys)
        {
            var transitionTemplate = transitions[key];

            var transistionType = key;

            var template = transitionTemplateService.GetById(transitionTemplate);

            if (template != null && template.Slots.Count != 0)
            {
                slots.Add(transistionType, [.. template.Slots.OrderBy(s => s.Index)]);
            }
        }

        return slots;
    }

    public static Dictionary<TransitionSlot, IDataSource> ResolveSources(Dictionary<TransitionSlot, IDataSource>? baseSources, Dictionary<TransitionSlot, IDataSource>? applyingSources)
    {
        var newSources = new Dictionary<TransitionSlot, IDataSource>();

        if(baseSources != null) baseSources.Keys.ToList().ForEach(x =>  newSources.Add(x, baseSources[x]));

        if(applyingSources != null) applyingSources.Keys.ToList().ForEach(x =>  newSources.Add(x, applyingSources[x]));

        return newSources;
    }
}