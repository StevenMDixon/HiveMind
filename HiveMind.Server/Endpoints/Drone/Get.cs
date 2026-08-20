using HiveMind.Server.Entities;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HiveMind.Server.Endpoints.Drone;

public class Get
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", Handle)
            .WithName("Get");
    }

    public static Results<Ok<List<Entities.Drone>>, NoContent, ValidationProblem> Handle(DroneService droneService)
    {
        var drones = droneService.GetAll();

        return TypedResults.Ok(drones);
    }
}
