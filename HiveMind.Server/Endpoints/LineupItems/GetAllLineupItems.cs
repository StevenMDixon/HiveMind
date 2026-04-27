using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HiveMind.Server.Endpoints.LineupItems;

public static class GetAllLineupItems
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", Handle).WithName("GetAllLineupItems");
    }

    public record LineupItem(int LineupItemId, int Index, string Type, int LineupId);
    public record GetAllLineupItemsResponse(List<LineupItem> LineupItems);
    public static Results<Ok<GetAllLineupItemsResponse>, NotFound> Handle(LineupItemService lineupItemService)
    {
        var lineupItems = lineupItemService.GetLineupItems();

        return TypedResults.Ok(new GetAllLineupItemsResponse(lineupItems.Select(x => new LineupItem(x.LineupItemId, x.Index, x.Type, x.LineupId)).ToList()));
    }
}
