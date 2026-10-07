
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;


namespace HiveMind.Server.Endpoints.Drone;

public class GetScheduleData
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/Schedules/{id:int}", Handle)
            .WithName("GetScheduleData");
    }

    public record DroneScheduleResults(string StationNumber, string ScheduleJson);

    public static Results<Ok<List<DroneScheduleResults>>, NoContent, ValidationProblem> Handle(DroneService droneService, [FromRoute] int id, [FromQuery] DateTime? date)
    {
        var requestedDate = date ?? DateTime.Today;

        var currentDateOnly = DateOnly.FromDateTime(requestedDate);

        var droneScheduleData = droneService.GetDroneSchedule(id, currentDateOnly);

        if (droneScheduleData.Count == 0)
        {
            return TypedResults.NoContent();
        }

        return TypedResults.Ok(droneScheduleData.Select(x => new DroneScheduleResults(x.Item1, x.Item2)).ToList());
    }
}
