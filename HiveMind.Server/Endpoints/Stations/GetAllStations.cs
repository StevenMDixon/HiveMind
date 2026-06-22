using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HiveMind.Server.Endpoints.Stations;

public static class GetAllStations
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", Handle);
    }
    
    public record Station(int StationId, string StationName, int StationNumber);
    public record GetAllStationsResponse(List<Station> Stations);

    public static Results<Ok<GetAllStationsResponse>, NotFound> Handle(StationService stationService)
    {
        var stations = stationService.GetAllStations();

        return TypedResults.Ok(new GetAllStationsResponse(stations.Select(x => new Station(x.StationId, x.StationName, x.StationNumber)).ToList()));
    }
}
