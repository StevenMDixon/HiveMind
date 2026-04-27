using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.LineupItems;

public static class GetLineupItemById
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:int}", Handle).WithName("GetLineupItemById");
    }

    public record QueryLineupItem(int QueryLineupItemId, int QueryId, int LineupItemId, int PlayDuration, int PlayCount, int PadTo, QueryType QueryType, PlayoutType PlayoutType, int Index);

    public record LineupItem(int LineupItemId, int Index, string name, string Type, int LineupId, ICollection<QueryLineupItem>? Queries);

    public static Results<Ok<LineupItem>, NotFound<string>> Handle(LineupItemService lineupItemService, [FromRoute] int id)
    {
        var lineupItem = lineupItemService.GetLineupItemByID(id);

        if (lineupItem is not null)
        {
            return TypedResults.Ok(new LineupItem(lineupItem.LineupItemId, lineupItem.Index, lineupItem.Name, lineupItem.Type, lineupItem.LineupId, lineupItem.Queries.Select(query => new QueryLineupItem(query.QueryLineupItemId, query.QueryId, query.LineupItemId, query.PlayDuration, query.PlayCount, query.PadTo, query.QueryType, query.PlayoutType, query.Index)).ToArray()));
        }

        return TypedResults.NotFound($"A lineup item with the ID: {id} was not found.");
    }
}
