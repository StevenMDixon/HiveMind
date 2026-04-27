using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Lineup;

public class DeleteLineup
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id:int}", Handle)
            .WithName("DeleteLineup");
    }

    public static Results<Ok, NotFound<string>> Handle(LineupService lineupService, [FromRoute] int id)
    {
        var lineup = lineupService.GetLineupByID(id);

        if (lineup is not null)
        {
            lineupService.DeleteLineup(id);
            return TypedResults.Ok();
        }

        return TypedResults.NotFound($"A lineup with the ID: {id} was not found.");
    }
}
