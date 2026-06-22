using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Stations
{
    public class DeleteStation
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapDelete("/{id:int}", Handle)
                .WithName("DeleteStation");
        }

        public static Results<Ok, NotFound<string>> Handle(StationService stationService, [FromRoute] int id)
        {
            var station = stationService.GetStationByID(id);

            if (station is not null)
            {
                stationService.Delete(id);
                return TypedResults.Ok();
            }

            return TypedResults.NotFound($"A station with the ID: {id} was not found.");
        }
    }
}
