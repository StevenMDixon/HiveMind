using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Entities;

namespace HiveMind.Server.Domain.Scheduler.SlotStrategy;

public class OutBumpStrategy: ISlotStrategy
{
    public List<GenerationResultItem> Generate(GenerationContext context, SourceItem source, string ShowName, int Duration, RetreiverType retreiverType)
    {
        var results = new List<MediaItem>();

        var retriever = context.Retriever;

        var bumpOutFilter = new List<(string, string, string)>
        {
            ("Duration", "LessThanEquals", (Duration).ToString()),
            ("Tag", "Equals", context.Settings["Bump Out Tag"] ?? "Out"),
            ("Tag", "Contains", ShowName)
        };

        var inBumpKey = context.Settings["Bump In Tag"] ?? "In";

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
            ("Duration", "LessThanEquals", (Duration - bumpOut.Duration).ToString()),
            ("Tag", "Equals", context.Settings["Bump In Tag"] ?? "In"),
            ("Tag", "Contains", ShowName)
        };

        var bumpIn = retriever.GetMedia(source, retreiverType, 1, bumpInFilter).FirstOrDefault();

        if (bumpIn != null) results.Add(bumpIn);

        return [.. results.Select(x => new GenerationResultItem() { MediaItem = x, Type = TransitionSlot.InBump, StartTime = 0, EndTime = x.Duration })];
    }
}
