using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Entities;

namespace HiveMind.Server.Domain.Scheduler.SlotStrategy;

public class OutBumpStrategy: ISlotStrategy
{
    public List<GenerationResultItem> Generate(GenerationContext context, SourceItem source, List<string> tags, int duration, RetreiverType retreiverType)
    {
        var results = new List<MediaItem>();

        var retriever = context.Retriever;

        var bumpOutFilter = new List<(string, string, string)>
        {
            ("Duration", "LessThanEquals", (duration).ToString()),
            ("Tag", "Equals", context.Settings["Bump Out Tag"] ?? "Out"),
        };

        if(tags.Count > 0) bumpOutFilter.AddRange(tags.Select(x => ("Tag", "Equals", x)));

        var bumpOut = retriever.GetMedia(source, retreiverType, 1, bumpOutFilter).FirstOrDefault();

        if (bumpOut == null)
        {
            bumpOutFilter[2] = ("Tag", "Equals", context.Settings["Bump Generic Tag"] ?? "Generic");

            bumpOut = retriever.GetMedia(source, retreiverType, 1, bumpOutFilter).FirstOrDefault();

            return bumpOut == null ? [] : [new GenerationResultItem() { MediaItem = bumpOut, Type = TransitionSlot.OutBump, StartTime = 0, EndTime = bumpOut.Duration }];
        }

        results.Add(bumpOut);

        // Try to get matching bump
        var bumpInFilter = new List<(string, string, string)>()
        {
            ("Duration", "LessThanEquals", (duration - bumpOut.Duration).ToString()),
            ("Tag", "Equals", context.Settings["Bump In Tag"] ?? "In")
        };

        if(tags.Count > 0) bumpInFilter.AddRange(tags.Select(x => ("Tag", "Equals", x)));

        var bumpIn = retriever.GetMedia(source, retreiverType, 1, bumpInFilter).FirstOrDefault();

        if (bumpIn != null) results.Add(bumpIn);

        return [.. results.Select(x => new GenerationResultItem() { MediaItem = x, Type = TransitionSlot.InBump, StartTime = 0, EndTime = x.Duration })];
    }
}
