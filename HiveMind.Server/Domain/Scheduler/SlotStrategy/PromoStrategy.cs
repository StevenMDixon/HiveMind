using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Entities;
using static HiveMind.Server.Domain.Enums.QueryEnums;

namespace HiveMind.Server.Domain.Scheduler.SlotStrategy;

public class PromoStrategy : ISlotStrategy
{
    public List<GenerationResultItem> Generate(GenerationContext context, SourceItem source, string showName, int Duration, RetreiverType retreiverType)
    {
        if(context.PromoQueries.Count != 0)
        {
            int randomIndex = Random.Shared.Next(context.PromoQueries.Count);

            var selectedQuery = context.PromoQueries[randomIndex];

            var customFilter = new List<(string, string, string)>()
            {
                (QueryAllowedFields.Duration.ToString(), QueryAllowedOperators.LessThanEquals.ToString(), Duration.ToString()),
            };

            var promo = context.Retriever.GetMedia(selectedQuery, retreiverType, 1, customFilter).FirstOrDefault();

            return promo != null ? [new GenerationResultItem() { MediaItem = promo, StartTime = 0, EndTime = promo.Duration, Type = TransitionSlot.Promo }] : [.. new List<GenerationResultItem>()];
        }

        return [.. new List<GenerationResultItem>()];
    }
}
