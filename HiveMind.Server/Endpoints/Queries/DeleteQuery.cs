using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Queries;

public class DeleteQuery
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id:int}", Handle)
            .WithName("DeleteQuery");
    }

    public static Results<Ok, NotFound<string>> Handle(QueryService queryService, [FromRoute] int id)
    {
        var query = queryService.GetQueryByID(id);

        if (query is not null)
        {
            queryService.Delete(id);
            return TypedResults.Ok();
        }

        return TypedResults.NotFound($"A query with the ID: {id} was not found.");
    }
}
