using HiveMind.Server.Domain.Scheduler.Sources;
using HiveMind.Server.Entities;
using static HiveMind.Server.Domain.Enums.QueryEnums;

namespace HiveMind.Server.Domain.Scheduler;

public static class MediaItemRetriever
{
    public static List<MediaItem> GetMedia(GenerationContext context, IDataSource dataSource, int? count = 1, List<(string, string, string)>? customFilters = null)
    {
        var query = dataSource.GetQuery(context.ScheduleDataProvider.Queries);
        
        if(query == null) return [];

        if(customFilters != null)
        {
            var filters = new List<QueryFilters>();

            filters.AddRange(query.Filters?.Select(x => x).ToList() ?? [] );

            filters.AddRange(customFilters.Select(x => new QueryFilters(){Field = Enum.Parse<QueryAllowedFields>(x.Item1), Operator = Enum.Parse<QueryAllowedOperators>(x.Item2), Value=x.Item3} ));

            query.Filters = filters;
        }


        var possibleItems = context.ScheduleDataProvider.Queries.GetMediaItemsByQuery(query);

        return possibleItems.OrderBy(r => Guid.NewGuid()).Take(count ?? 1).ToList();
    }
}
