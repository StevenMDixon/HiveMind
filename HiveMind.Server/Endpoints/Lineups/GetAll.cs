using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HiveMind.Server.Endpoints.Lineup;

public static class GetAll
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", Handle).WithName("GetAllLineups");
    }

    public record Lineup(int Id, string Name, TimeOnly StartTime);
    public record GetAllLineupsResponse(List<Lineup> Lineups);

    public record Parameters(
       string? Search,
       int? Page,
       int? Count
    );

    public static Results<Ok<GetAllLineupsResponse>, NotFound> Handle(LineupService lineupService, [AsParameters] Parameters filters)
    {
        var lineups = lineupService.GetAllLineups();

        var pageSize = filters.Count ?? 100;
        var currentPage = filters.Page ?? 1;
        var dataCount = lineups.Count();

        if (filters.Search != null && filters.Search != string.Empty) lineups = lineups.Where(x => x.Name.ToLower().Contains(filters.Search.ToLower()));

        var m = (pageSize * currentPage);
        lineups = lineups.Skip((pageSize * (currentPage - 1))).Take(pageSize);

        return TypedResults.Ok(new GetAllLineupsResponse(lineups.Select(x => new Lineup(x.Id, x.Name, x.StartTime)).ToList()));
    }
}
