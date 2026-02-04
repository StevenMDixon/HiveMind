using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.ScheduleItems;

public static class GetScheduleItemsByScheduleId
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/schedule/{scheduleId:int}", Handle).WithName("GetScheduleItemsByScheduleId");
    }

    public record ScheduleItem(int ScheduleItemId, int Index, string Type, int ScheduleId);
    public record GetScheduleItemsResponse(List<ScheduleItem> ScheduleItems);

    public static Results<Ok<GetScheduleItemsResponse>, NotFound<string>> Handle(ScheduleItemService scheduleItemService, [FromRoute] int scheduleId)
    {
        var scheduleItems = scheduleItemService.GetScheduleItemsByScheduleID(scheduleId);

        if (scheduleItems is not null)
        {
            return TypedResults.Ok(new GetScheduleItemsResponse(scheduleItems.Select(x => new ScheduleItem(x.ScheduleItemId, x.Index, x.Type, x.ScheduleId)).ToList()));
        }

        return TypedResults.NotFound($"Schedule items for schedule ID: {scheduleId} were not found.");
    }
}
