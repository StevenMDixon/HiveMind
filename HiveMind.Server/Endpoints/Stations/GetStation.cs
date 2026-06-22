using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Stations;

public class GetStation
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:int}", Handle);
    }

    public record Station(int StationId, string StationName, int StationNumber);
    public record GetStationsResponse(Station Station);

    public static Results<Ok<GetStationsResponse>, NotFound<string>> Handle(StationService stationService, [FromRoute] int id)
    {
        var station = stationService.GetStationByID(id);
        if (station == null) return TypedResults.NotFound($"A station with the ID: {id} was not found.");

        return TypedResults.Ok(new GetStationsResponse(new Station(station.StationId, station.StationName, station.StationNumber)));
    }
}
