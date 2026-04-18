using HiveMind.Server.Entities;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HiveMind.Server.Endpoints.Queries;

public static class GetAllQueries
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", Handle);
    }

    public record Query(int QueryId, string QueryName);
    public record GetAllQueriesResponse(List<Query> Queries);

    public static Results<Ok<GetAllQueriesResponse>, NotFound> Handle(QueryService queryService)
    {
        var queries = queryService.GetAllQueries();

        return TypedResults.Ok(new GetAllQueriesResponse(queries.Select(x => new Query(x.QueryId, x.Name)).ToList()));
    }
}
