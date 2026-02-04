using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.ScheduleItems;

public static class GetScheduleItemById
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:int}", Handle).WithName("GetScheduleItemById");
    }

    public record ScheduleItem(int ScheduleItemId, int Index, string Type, int ScheduleId);

    public static Results<Ok<ScheduleItem>, NotFound<string>> Handle(ScheduleItemService scheduleItemService, [FromRoute] int id)
    {
        var scheduleItem = scheduleItemService.GetScheduleItemByID(id);

        if (scheduleItem is not null)
        {
            return TypedResults.Ok(new ScheduleItem(scheduleItem.ScheduleItemId, scheduleItem.Index, scheduleItem.Type, scheduleItem.ScheduleId));
        }

        return TypedResults.NotFound($"A schedule item with the ID: {id} was not found.");
    }
}
