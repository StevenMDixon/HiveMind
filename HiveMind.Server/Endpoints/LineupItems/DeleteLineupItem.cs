using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.LineupItems;

public class DeleteLineupItem
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id:int}", Handle)
            .WithName("DeleteLineupItem");
    }

    public static Results<Ok, NotFound<string>> Handle(LineupItemService lineupItemService, [FromRoute] int id)
    {
        var lineupItem = lineupItemService.GetLineupItemByID(id);

        if (lineupItem is not null)
        {
            lineupItemService.DeleteLineupItem(id);
            return TypedResults.Ok();
        }

        return TypedResults.NotFound($"A lineup item with the ID: {id} was not found.");
    }
}
