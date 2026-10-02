using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Entities;

namespace HiveMind.Server.Domain.Scheduler.SelectionStrategy;

public class SingleStrategy : ISelectionStrategy
{
    public List<MediaItem> Generate(GenerationContext context, List<string> Queries, int Duration , RetreiverType retreiverType, string ShowName)
    {
        if(ShowName != null && ShowName != string.Empty)
        {
            Queries.Insert(0, "Show, Equals, " + ShowName);
        }

        return context.Retriever.GetMedia([.. Queries], retreiverType);
    }
}
