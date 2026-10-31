using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Drone;

public class Update
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("/", Handle)
            .WithName("Update");
    }

    public record UpdateRequest(int Id, string? HostName, string? Name, int? Slots, List<int>? Stations);

    public static Results<Ok, NoContent, ValidationProblem, NotFound> Handle(DroneService droneService, StationService stationService, [FromBody] UpdateRequest updateRequest)
    {
        var drone = droneService.GetById(updateRequest.Id);

        if (drone == null)
        {
            return TypedResults.NotFound();
        }

        drone.HostName = updateRequest.HostName ?? drone.HostName;
        drone.Name = updateRequest.Name ?? drone.Name ;
        drone.StationSlots = updateRequest.Slots ?? drone.StationSlots;

        if(updateRequest.Stations != null)
        {
            var stations = stationService.GetStationsByIds(updateRequest.Stations);

            drone.Stations = stations.ToList();
        }

        droneService.Update(drone);

        return TypedResults.Ok();
    }
}

