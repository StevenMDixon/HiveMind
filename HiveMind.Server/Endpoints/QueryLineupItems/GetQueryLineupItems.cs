using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HiveMind.Server.Endpoints.QueryLineupItems;

public class GetQueryLineupItems
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", Handle);
    }

    public record QueryLineupItem(int QueryLineupItemId, int QueryId, int LineupItemId, int PlayDuration, int PlayCount, int PadTo, QueryType QueryType, PlayoutType PlayoutType, int Index);
    public record LineupItemResponse(List<QueryLineupItem> Items);

    public static Results<Ok<LineupItemResponse>, NotFound<string>> Handle(QueryLineupItemService queryLineupItemService)
    {
        var queryLineupItems = queryLineupItemService.GetAllQueryLineupItems();
       
        return TypedResults.Ok(new LineupItemResponse(queryLineupItems.Select(x => new QueryLineupItem(x.QueryLineupItemId, x.QueryId, x.LineupItemId, x.PlayDuration, x.PlayCount, x.PadTo, x.QueryType, x.PlayoutType, x.Index)).ToList()));
    }
}
