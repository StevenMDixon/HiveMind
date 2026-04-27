using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.QueryLineupItems;

public class DeleteQueryLineupItem
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id:int}", Handle)
            .WithName("DeleteQueryLineupItem");
    }

    public static Results<Ok, NotFound<string>> Handle(QueryLineupItemService queryLineupItemService, [FromRoute] int id)
    {
        var item = queryLineupItemService.GetQueryLineupItemByID(id);

        if (item is not null)
        {
            queryLineupItemService.Delete(id);
            return TypedResults.Ok();
        }

        return TypedResults.NotFound($"A query lineup item with the ID: {id} was not found.");
    }
}
