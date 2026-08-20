using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Entities;

namespace HiveMind.Server.Domain.Scheduler.SelectionStrategy;

public class FillStrategy: ISelectionStrategy
{
    public List<MediaItem> Generate(GenerationContext context, List<string> Queries, int Duration, RetreiverType retreiverType, string ShowName)
    {
        var remainingDuration = (Duration * 60 * 1000);

        var results = new List<MediaItem>();

        var customQueries = new List<(string, string, string)>
        {
            ("Show", "Equals", ShowName)
        };

        while (remainingDuration > 0)
        {
            var derivedQueries = new List<String>()
            {
                "Duration, LessThan, " + remainingDuration.ToString()
            };

            var fillQueryResults = context.Retriever.GetMedia(derivedQueries.ToArray(), retreiverType, 1, customQueries);

            if (fillQueryResults.Any())
            {
                remainingDuration -= fillQueryResults.Sum(m => m.Duration);

                if (remainingDuration <= 0) break;

                results.AddRange(fillQueryResults);
            } 
            else
            {
                break;
            }
        }

        return results;
    }
}
