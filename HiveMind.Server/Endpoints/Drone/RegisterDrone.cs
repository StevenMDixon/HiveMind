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

    public record RegisterRequest(string hostName);

    public record RegisterResponse(int Id);

    public static Results<Ok<RegisterResponse>, NoContent, ValidationProblem> Handle(DroneService droneService, [FromBody] RegisterRequest request)
    {
        var drone = droneService.GetByHostName(request.hostName);

        if (drone == null) return TypedResults.NoContent();

        return TypedResults.Ok(new RegisterResponse(drone.Id));
    }
}
