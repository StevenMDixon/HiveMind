using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.QueryLineupItems;

public static class GetQueryLineupItemById
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:int}", Handle).WithName("GetQueryLineupItemById");
    }

    public record QueryLineupItem(int QueryLineupItemId, int QueryId, int LineupItemId, int PlayDuration, int PlayCount, int PadTo, QueryType QueryType, PlayoutType PlayoutType, int Index);

    public static Results<Ok<QueryLineupItem>, NotFound<string>> Handle(QueryLineupItemService queryLineupItemService, [FromRoute] int id)
    {
        var item = queryLineupItemService.GetQueryLineupItemByID(id);

        if (item is not null)
        {
            return TypedResults.Ok(new QueryLineupItem(item.QueryLineupItemId, item.QueryId, item.LineupItemId, item.PlayDuration, item.PlayCount, item.PadTo, item.QueryType, item.PlayoutType, item.Index));
        }

        return TypedResults.NotFound($"A query lineup item with the ID: {id} was not found.");
    }
}
