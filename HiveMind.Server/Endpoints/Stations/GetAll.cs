using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HiveMind.Server.Endpoints.Stations;

public static class GetAll
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", Handle);
    }
    
    public record Station(int Id, string Name, int Number, string Logo, int? StrategyId, int? DroneId);
    public record GetAllStationsResponse(List<Station> Stations);

    public static Results<Ok<GetAllStationsResponse>, NotFound> Handle(StationService stationService)
    {
        var stations = stationService.GetAllStations();

        return TypedResults.Ok(new GetAllStationsResponse(stations.Select(x => new Station(x.Id, x.Name, x.Number, x.Logo, x.StrategyId, x.DroneId)).ToList()));
    }
}
