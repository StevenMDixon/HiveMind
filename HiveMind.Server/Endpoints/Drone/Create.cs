using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Drone;

public class Create
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", Handle)
            .WithName("Create");
    }

    public record CreateRequest(string hostName, string Name, int Slots);

    public static Results<Ok, NoContent, ValidationProblem> Handle(DroneService droneService, [FromBody] CreateRequest createRequest)
    {
        droneService.Create(createRequest.Name, createRequest.hostName, createRequest.Slots);
        return TypedResults.Ok();
    }
}
