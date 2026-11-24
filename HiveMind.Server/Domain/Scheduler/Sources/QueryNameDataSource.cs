using HiveMind.Server.Entities;
using HiveMind.Server.Services;


namespace HiveMind.Server.Domain.Scheduler.Sources;

public class QueryNameDataSource: IDataSource
{
    public required string Name;

    public Query? GetQuery(IQueryService queryService)
    {
        var query = queryService.GetQueryByName(Name);

        return query;
    }
}