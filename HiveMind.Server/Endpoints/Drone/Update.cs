using HiveMind.Server.Entities;
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

    public record UpdateRequest(int Id, string HostName, string Name, int Slots, List<UpdatedStation> Stations);

    public record UpdatedStation(int Id);

    public static Results<Ok, NoContent, ValidationProblem> Handle(DroneService droneService, [FromBody] UpdateRequest updateRequest)
    {
        var drone = droneService.GetById(updateRequest.Id);

        if (drone == null)
        {
            return TypedResults.NoContent();
        }

        drone.HostName = updateRequest.HostName;
        drone.Name = updateRequest.Name;
        drone.StationSlots = updateRequest.Slots;

        //var itemsToUpdate = new List<(Station, UpdatedStation)>();

        //foreach (var station in updateRequest.Stations ?? [])
        //{
        //    var matchedItem = drone?.Stations?.Where(x => x.Id == station.Id).FirstOrDefault();

        //    if (matchedItem != null)
        //    {
        //        itemsToUpdate.Add((matchedItem, station));
        //    }

        //    itemsToUpdate.Add((new Station()
        //    {

        //    }, station));
        //}

        //drone!.Stations = Helper.Resolve(itemsToUpdate, UpdateItem);

        droneService.Update(drone);

        return TypedResults.Ok();
    }

    private static Station UpdateItem(Station station, UpdatedStation updatedStation)
    {
        

        return station;
    }
}

