
using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Lineup;

public static class GetLineupById
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:int}", Handle).WithName("GetLineupById");
    }

    public record QueryLineupItem(int QueryLineupItemId, int QueryId, int LineupItemId, int PlayDuration, int PlayCount, int PadTo, QueryType QueryType, PlayoutType PlayoutType, int Index);

    public record LineupItem(int LineupItemId, int Index, string Type, string Name, int LineupId, ICollection<QueryLineupItem> Queries);

    public record Lineup(int LineupId, string LineupName, TimeOnly StartTime, ICollection<LineupItem> LineupItems);

    public static Results<Ok<Lineup>, NotFound<string>> Handle(LineupService lineupService, [FromRoute] int id)
    {
        var lineup = lineupService.GetLineupByID(id);

        if (lineup is not null)
        {
            return TypedResults.Ok(new Lineup(
                lineup.LineupId, 
                lineup.LineupName, 
                lineup.StartTime, 
                lineup.LineupItems?.Select(item => new LineupItem(item.LineupItemId, item.Index, item.Type, item.Name, item.LineupId, item.Queries?.Select(c => new QueryLineupItem(c.QueryLineupItemId, c.QueryId, c.LineupItemId, c.PlayDuration, c.PlayCount, c.PadTo, c.QueryType, c.PlayoutType, c.Index)).ToList() ?? new List<QueryLineupItem>())).ToList() ?? new List<LineupItem>()));
        }

        return TypedResults.NotFound($"A lineup with the ID: {id} was not found.");
    }
}
