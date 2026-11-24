using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Entities;
using HiveMind.Server.QueryEngine;
using HiveMind.Server.Services;
using static HiveMind.Server.Domain.Enums.QueryEnums;

namespace HiveMind.Server.Domain.Scheduler.Sources;
public class CustomDataSource : IDataSource
{
    public required List<string> Queries = new List<string>();

    public Query? GetQuery(IQueryService queryService)
    {
        var splitQueries = Queries.Select(x => x.Split(", "));

        var query = new Query()
        {
            Id = -1,
            Filters = splitQueries.Select(x => new QueryFilters(){Field = Enum.Parse<QueryAllowedFields>(x[0]), Operator = Enum.Parse<QueryAllowedOperators>(x[1]), Value=x[2]} ).ToList()
        };  

        return query;
    }
} 