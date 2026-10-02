using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Drone;

public class RegisterDrone
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/register", Handle)
            .WithName("Register");
    }

    public record ResigterRequest(string hostName);

    public record RegisterResponse(int id);

    public static Results<Ok<RegisterResponse>, NoContent, ValidationProblem> Handle(DroneService droneService, [FromBody] ResigterRequest request)
    {
        var drone = droneService.GetByHostName(request.hostName);

        if (drone == null) return TypedResults.NoContent();

        return TypedResults.Ok(new RegisterResponse(drone.DroneId));
    }
}
