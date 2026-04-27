using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.LineupItems;

public static class GetItemsByLineupId
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/lineup/{lineupId:int}", Handle).WithName("GetLineupItemsByLineupId");
    }

    public record LineupItem(int LineupItemId, int Index, string Type, int LineupId);
    public record GetLineupItemsResponse(List<LineupItem> LineupItems);
    public static Results<Ok<GetLineupItemsResponse>, NotFound<string>> Handle(LineupItemService lineupItemService, [FromRoute] int lineupId)
    {
        var lineupItems = lineupItemService.GetLineupItemsByLineupID(lineupId);

        if (lineupItems is not null)
        {
            return TypedResults.Ok(new GetLineupItemsResponse(lineupItems.Select(x => new LineupItem(x.LineupItemId, x.Index, x.Type, x.LineupId)).ToList()));
        }

        return TypedResults.NotFound($"Lineup items for lineup ID: {lineupId} were not found.");
    }
}
