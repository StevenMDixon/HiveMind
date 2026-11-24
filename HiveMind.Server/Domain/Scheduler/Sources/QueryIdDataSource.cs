using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Entities;
using HiveMind.Server.QueryEngine;
using HiveMind.Server.Services;

namespace HiveMind.Server.Domain.Scheduler.Sources;
class QueryIdDataSource : IDataSource
{
    public required int Id;

    public Query? GetQuery(IQueryService queryService)
    {
        var query = queryService.GetQueryByID(Id);

        return query;
    }
} 