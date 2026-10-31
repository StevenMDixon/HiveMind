using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Drone;

public class Get
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:int}", Handle)
            .WithName("GetDroneById");
    }

    public record Drone(int Id, string Name, string HostName, List<Station> Stations);

    public record Station(int Id, string Name);

    public static Results<Ok<Drone>, NotFound<string>> Handle(DroneService droneService, [FromRoute] int id)
    {
        var drone = droneService.GetById(id);
       
        if (drone is not null)
        {
            return TypedResults.Ok(new Drone(drone.Id, drone.Name, drone.HostName, drone?.Stations?.Select(x => new Station(x.Id, x.Name)).ToList() ?? []));
        }

        return TypedResults.NotFound($"A drone with the ID: {id} was not found.");
    }
}
