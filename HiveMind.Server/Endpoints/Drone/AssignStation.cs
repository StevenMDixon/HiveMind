using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Drone;

public class AssignStation
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/Stations", Handle)
            .WithName("Assign Stations");
    }

    public record StationAssigmentRequest(List<int> StationIds);

    public static Results<Ok, NoContent, ValidationProblem> Handle(DroneService droneService, [FromBody] StationAssigmentRequest stationAssigmentRequest)
    {
        


        return TypedResults.Ok();
    }
}
