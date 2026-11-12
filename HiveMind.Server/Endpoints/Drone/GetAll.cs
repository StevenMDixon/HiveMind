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

    public record Drone(int Id, string Name, string HostName, List<Station> Stations);

    public record Station(int Id, string Name, string Logo);

    public static Results<Ok<List<Drone>>, NoContent> Handle(DroneService droneService)
    {
        var drones = droneService.GetAll();

        return TypedResults.Ok(drones.Select(x => new Drone(x.Id, x.Name, x.HostName, x.Stations?.Select(y => new Station(y.Id, y.Name, y.Logo)).ToList() ?? [])).ToList());
    }
}
