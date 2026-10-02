using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Domain.Scheduler.SlotStrategy;
using HiveMind.Server.Entities;

namespace HiveMind.Server.Domain.Scheduler.Nodes;

public class BreakNode: INode
{
    public INode Slot { get; set; } = null!;

    public IEnumerable<GenerationResultItem> Generate(GenerationContext context)
    {
        var results = new List<GenerationResultItem>();

        var nodeResults = Slot.Generate(context);

        var currentBlock = context.BlockContext.Last();

        var currentProgram = currentBlock.ProgramContexts.Last();

        currentProgram.RemainingDuration -= nodeResults.Sum(x => x.EndTime - x.StartTime);

        var breakBudget = currentProgram.RemainingDuration / (nodeResults.Count());

        for (var i = 0; i < nodeResults.Count(); i++)
        {
            var breakType = i == nodeResults.Count() - 1 ? TransitionType.Postroll : TransitionType.Midroll;

            var breakResults = ResolveSlots(context, breakBudget, breakType, currentBlock.Sources, currentBlock.Transitions);

            results.Add(nodeResults.ElementAt(i));

            results.AddRange(breakResults);
        }

        return results;
    }

    private List<GenerationResultItem> ResolveSlots(GenerationContext context, int fillDuration, TransitionType type, Dictionary<TransitionSlot, SourceItem> sources, Dictionary<TransitionType, List<TransitionTemplateSlot>> slots)
    {
        var results = new List<GenerationResultItem>();

        // Set Promos from context
        var random = Random.Shared.Next(context.PromoQueries.Count);

        sources[TransitionSlot.Promo] = context.PromoQueries[random];

        var breakSlots = slots.ContainsKey(type) ? slots[type] : new List<TransitionTemplateSlot>();

        if (breakSlots.Any())
        {
            results.AddRange(GenerateBreak(context, fillDuration, breakSlots, sources));
        }
        else
        {
            var fillerMedia = new Dictionary<TransitionSlot, SourceItem> { { TransitionSlot.Filler, sources.ContainsKey(TransitionSlot.Filler) ? sources[TransitionSlot.Filler] : new SourceItem() } };

            results.AddRange(GenerateBreak(context, fillDuration, new List<TransitionTemplateSlot> { new TransitionTemplateSlot() { Slot = TransitionSlot.Filler } }, fillerMedia));
        }

        return results;
    }

    private static List<GenerationResultItem> GenerateBreak(GenerationContext context, int fillDuration, List<TransitionTemplateSlot> slots, Dictionary<TransitionSlot, SourceItem> slotMedia)
    {
        var results = new List<GenerationResultItem>();

        var remainingDuration = fillDuration;

        var fillerLocation = new List<int>();

        var filledSlots = slots.Select(x => new List<GenerationResultItem>()).ToList();

        var matchedBumps = new List<GenerationResultItem>();

        var currentBlockContext = context.BlockContext.Last();

        var currentProgram = currentBlockContext.ProgramContexts.Last();

        var currentShow = currentProgram.UseBlockName ? currentBlockContext.Name : currentProgram.ProgramName;

        for (var i = 0; i < slots.Count; i++)
        {
            var slot = slots[i];

            if (slot.Slot.Equals(TransitionSlot.Filler))
            {
                fillerLocation.Add(i);
                continue;
            }

            var mappedSlot = slot.Slot;

            if (mappedSlot.Equals(TransitionSlot.InBump) || mappedSlot.Equals(TransitionSlot.OutBump)) mappedSlot = TransitionSlot.Bump;

            if (!slotMedia.ContainsKey(mappedSlot))
            {
                //if we don't have a source use the filler!
                fillerLocation.Add(i);
                continue;
            }

            // figure out how to get Coming up next promos
            var slotStrategy = SlotStrategyResolver.Resolve(slot.Slot);

            var nodeEvents = slotStrategy.Generate(context, slotMedia[mappedSlot], currentShow, remainingDuration, RetreiverType.Random);

            if (nodeEvents.Any())
            {
                GenerationResultItem? selectedMedia = null;

                if (slot.Slot.Equals(TransitionSlot.OutBump))
                {
                    selectedMedia = nodeEvents.First();

                    if (nodeEvents.Count() > 1 && matchedBumps.Count < slots.Count(x => x.Slot.Equals(TransitionSlot.OutBump)))
                    {
                        var inBump = nodeEvents.ElementAt(1);
                        remainingDuration -= inBump.MediaItem.Duration;
                        matchedBumps.Add(inBump);
                    }
                }
                else if (slot.Slot.Equals(TransitionSlot.InBump) && matchedBumps.Any())
                {
                    selectedMedia = matchedBumps.Last();
                    matchedBumps.RemoveAt(matchedBumps.Count - 1);
                    filledSlots[i].AddRange(selectedMedia);
                    continue;
                }
                else
                {
                    selectedMedia = nodeEvents.First();
                }

                if (selectedMedia != null)
                {
                    remainingDuration -= selectedMedia.EndTime;
                    filledSlots[i].AddRange(selectedMedia);
                }
            }
        }

        // fill in the filler :-)
        if (fillerLocation.Any())
        {
            var remainingFillDuration = remainingDuration / fillerLocation.Count();

            var refundedTime = 0;

            for (int i = 0; i < fillerLocation.Count(); i++)
            {
                var needingFiller = fillerLocation[i];

                var fillerStrategy = i == fillerLocation.Count() - 1 ? SlotStrategyResolver.Resolve(TransitionSlot.CutFiller) : SlotStrategyResolver.Resolve(TransitionSlot.Filler);

                var filled = fillerStrategy.Generate(context, slotMedia[TransitionSlot.Filler], currentShow, remainingFillDuration + refundedTime, RetreiverType.Random);

                // Unused time is refunded on normal filler blocks so that there are no odd cuts in the middle of a filler block.  CutFiller blocks do not refund time as they are the last filler block and should be filled to the end of the break.
                refundedTime = remainingFillDuration - filled.Sum(x => x.EndTime - x.StartTime);

                filledSlots.Insert(needingFiller, filled);
            }
        }

        results.AddRange(filledSlots.SelectMany(x => x));
        return results;
    }
}
