using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Stations;

public class Create
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", Handle).WithName("CreateStation");
    }

    public record StationRequest(string Name, int Number, string Logo);

    public static Results<Ok, NoContent> Handle(StationService stationService, [FromBody] StationRequest request)
    {

        var newStation = new Entities.Station
        {
            Name = request.Name,
            Number = request.Number
        };

        stationService.AddStation(newStation);

        return TypedResults.NoContent();
    }
}
