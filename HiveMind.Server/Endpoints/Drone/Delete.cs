using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Drone;

public class Delete
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id:int}", Handle)
            .WithName("Delete");
    }

    public static Results<Ok, NoContent, ValidationProblem> Handle(DroneService droneService, [FromRoute] int id)
    {
        var drone = droneService.GetById(id);
        if (drone == null)
        {
            return TypedResults.NoContent();
        }

        droneService.Delete(id);
        return TypedResults.Ok();
    }
}
