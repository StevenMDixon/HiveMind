using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.QueryScheduleItems;

public class GetAllQueryScheduleItems
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", Handle);
    }

    public record QueryScheduleItem(int QueryScheduleItemId, int QueryId, int ScheduleItemId, int PlayDuration, int PlayCount, int PadTo, QueryType QueryType, PlayoutType PlayoutType, int Index);
    public record ScheduleItemResponse(List<QueryScheduleItem> Items);

    public static Results<Ok<ScheduleItemResponse>, NotFound<string>> Handle(QueryScheduleItemService queryScheduleItemService)
    {
        var queryScheduleItems = queryScheduleItemService.GetAllQueryScheduleItems();
       
        return TypedResults.Ok(new ScheduleItemResponse(queryScheduleItems.Select(x => new QueryScheduleItem(x.QueryScheduleItemId, x.QueryId, x.ScheduleItemId, x.PlayDuration, x.PlayCount, x.PadTo, x.QueryType, x.PlayoutType, x.Index)).ToList()));
    }
}
