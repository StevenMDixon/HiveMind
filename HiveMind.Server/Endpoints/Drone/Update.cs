using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Drone
{
    public class Update
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapPut("/", Handle)
                .WithName("Update");
        }

        public record UpdateRequest(int id, string hostName, string name, int slots);

        public static Results<Ok, NoContent, ValidationProblem> Handle(DroneService droneService, [FromBody] UpdateRequest updateRequest)
        {
            var drone = droneService.GetById(updateRequest.id);
            if (drone == null)
            {
                return TypedResults.NoContent();
            }

            drone.HostName = updateRequest.hostName;
            drone.Name = updateRequest.name;
            drone.StationSlots = updateRequest.slots;

            droneService.Update(drone);

            return TypedResults.Ok();
        }
    }
}
