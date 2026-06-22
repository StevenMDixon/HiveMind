using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Stations;

public class CreateStation
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", Handle).WithName("CreateStation");
    }

    public record StationRequest(string StationName, int StationNumber);

    public static Results<Ok, NoContent> Handle(StationService stationService, [FromBody] StationRequest request)
    {

        var newStation = new Entities.Station
        {
            StationName = request.StationName,
            StationNumber = request.StationNumber
        };

        stationService.AddStation(newStation);

        return TypedResults.NoContent();
    }
}
