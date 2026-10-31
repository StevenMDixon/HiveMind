using HiveMind.Server.Services;

using HiveMind.Server.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Queries;

public class QueryTest
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/test/{id:int}", Handle).WithName("TestQuery");
    }

    public record QueryResult(ICollection<MediaItem> MediaItems);

    public static Results<Ok<QueryResult>, NotFound<string>> Handle(QueryService queryService, [FromRoute] int id)
    {
        var queryResults = queryService.GetMediaItemsByQueryId(id).Take(20);

        if (queryResults is not null)
        {
            return TypedResults.Ok(new QueryResult(queryResults.ToList()));
        }

        return TypedResults.NotFound($"A query with the ID: {id} was not found.");
    }
}
