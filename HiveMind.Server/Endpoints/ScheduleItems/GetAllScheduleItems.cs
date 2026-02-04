using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HiveMind.Server.Endpoints.ScheduleItems;

public static class GetAllScheduleItems
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", Handle).WithName("GetAllScheduleItems");
    }

    public record ScheduleItem(int ScheduleItemId, int Index, string Type, int ScheduleId);
    public record GetAllScheduleItemsResponse(List<ScheduleItem> ScheduleItems);

    public static Results<Ok<GetAllScheduleItemsResponse>, NotFound> Handle(ScheduleItemService scheduleItemService)
    {
        var scheduleItems = scheduleItemService.GetAllScheduleItems();

        return TypedResults.Ok(new GetAllScheduleItemsResponse(scheduleItems.Select(x => new ScheduleItem(x.ScheduleItemId, x.Index, x.Type, x.ScheduleId)).ToList()));
    }
}
