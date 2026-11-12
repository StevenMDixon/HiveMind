using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HiveMind.Server.Endpoints.Stations;

public static class GetAll
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", Handle)
            .WithName("GetAllStations");
    }
    
    public record Station(int Id, string Name, int Number, string Logo, int? StrategyId);
    public record GetAllStationsResponse(List<Station> Stations, int CurrentPage, int PageSize, int DataCount);

    public record Parameters(
       string? Search,
       int? Page,
       int? Count
    );

    public static Results<Ok<GetAllStationsResponse>, NotFound> Handle(StationService stationService, [AsParameters] Parameters filters)
    {
        var stations = stationService.GetAllStations();

        var pageSize = filters.Count ?? 100;
        var currentPage = filters.Page ?? 1;
        var dataCount = stations.Count();

        if (filters.Search != null && filters.Search != string.Empty) stations = stations.Where(x => x.Name.ToLower().Contains(filters.Search.ToLower()));

        var m = (pageSize * currentPage);
        stations = stations.Skip((pageSize * (currentPage - 1))).Take(pageSize);

        return TypedResults.Ok(new GetAllStationsResponse(stations.Select(x => new Station(x.Id, x.Name, x.Number, x.Logo, x.StrategyId)).ToList(), currentPage, pageSize, dataCount));
    }
}
