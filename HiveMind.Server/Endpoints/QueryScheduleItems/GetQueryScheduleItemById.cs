using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.QueryScheduleItems;

public static class GetQueryScheduleItemById
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:int}", Handle).WithName("GetQueryScheduleItemById");
    }

    public record QueryScheduleItem(int QueryScheduleItemId, int QueryId, int ScheduleItemId, int PlayDuration, int PlayCount, int PadTo, QueryType QueryType, PlayoutType PlayoutType, int Index);

    public static Results<Ok<QueryScheduleItem>, NotFound<string>> Handle(QueryScheduleItemService queryScheduleItemService, [FromRoute] int id)
    {
        var item = queryScheduleItemService.GetQueryScheduleItemByID(id);

        if (item is not null)
        {
            return TypedResults.Ok(new QueryScheduleItem(item.QueryScheduleItemId, item.QueryId, item.ScheduleItemId, item.PlayDuration, item.PlayCount, item.PadTo, item.QueryType, item.PlayoutType, item.Index));
        }

        return TypedResults.NotFound($"A query schedule item with the ID: {id} was not found.");
    }
}
