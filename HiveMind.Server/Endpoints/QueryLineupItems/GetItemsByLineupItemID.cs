using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.QueryLineupItems;

public class GetItemsByLineupItemID
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/lineupItem/{id:int}", Handle).WithName("GetItemsByLineupItemID");
    }

    public record QueryLineupItem(int QueryLineupItemId, int QueryId, int LineupItemId, int PlayDuration, int PlayCount, int PadTo, QueryType QueryType, PlayoutType PlayoutType, int Index);
    public record LineupItemResponse(List<QueryLineupItem> Items);
    public static Results<Ok<LineupItemResponse>, NotFound<string>> Handle(QueryLineupItemService queryLineupItemService, [FromRoute] int id)
    {
        var queryLineupItems = queryLineupItemService.GetQueryLineupItemsByLineupItemID(id);

        if (queryLineupItems is not null)
        {
            return TypedResults.Ok(new LineupItemResponse(queryLineupItems.Select(x => new QueryLineupItem(x.QueryLineupItemId, x.QueryId, x.LineupItemId, x.PlayDuration, x.PlayCount, x.PadTo, x.QueryType, x.PlayoutType, x.Index)).ToList()));
        }

        return TypedResults.NotFound($"A query lineup item with the ID: {id} was not found.");
    }
}
