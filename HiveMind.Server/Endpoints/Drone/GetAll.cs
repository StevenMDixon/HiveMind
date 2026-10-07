using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HiveMind.Server.Endpoints.Drone;

public class GetAll
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", Handle)
            .WithName("GetAll");
    }

    public static Results<Ok<List<Entities.Drone>>, NoContent> Handle(DroneService droneService)
    {
        var drones = droneService.GetAll();

        return TypedResults.Ok(drones);
    }
}
