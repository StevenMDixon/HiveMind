using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Domain.Scheduler.Nodes;
using HiveMind.Server.Entities;
using static HiveMind.Server.Domain.Enums.QueryEnums;

namespace HiveMind.Server.Domain.Scheduler.SlotStrategy;

public class PromoStrategy : ISlotStrategy
{
    public List<GenerationResultItem> Generate(GenerationContext context, int QueryId, string showName, int Duration, RetreiverType retreiverType)
    {
        if(context.PromoQueries.Any())
        {
            int randomIndex = Random.Shared.Next(context.PromoQueries.Count);

            var selectedQuery = context.PromoQueries[randomIndex];

            var customFilter = new List<(string, string, string)>()
            {
                (QueryAllowedFields.Duration.ToString(), QueryAllowedOperators.LessThanEquals.ToString(), Duration.ToString()),
                ("Show", "Contains", selectedQuery.Item2)
            };

            var promo = context.Retriever.GetMedia(selectedQuery.Item1, retreiverType, 1, customFilter).First();

            return promo != null ? new List<GenerationResultItem> { new GenerationResultItem() { MediaItem = promo, StartTime = 0, EndTime = promo.Duration, Type = TransitionSlot.Promo } } : new List<GenerationResultItem>();
        }

        return new List<GenerationResultItem>();
    }
}
