using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HiveMind.Server.Endpoints.Lineup;

public static class GetAllLineups
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", Handle).WithName("GetAllLineups");
    }

    public record Lineup(int LineupId, string LineupName, TimeOnly StartTime);
    public record GetAllLineupsResponse(List<Lineup> Lineups);
    public static Results<Ok<GetAllLineupsResponse>, NotFound> Handle(LineupService lineupService)
    {
        var lineups = lineupService.GetAllLineups();

        return TypedResults.Ok(new GetAllLineupsResponse(lineups.Select(x => new Lineup(x.LineupId, x.LineupName, x.StartTime)).ToList()));
    }
}
