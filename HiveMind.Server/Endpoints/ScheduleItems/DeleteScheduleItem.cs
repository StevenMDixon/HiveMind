using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.ScheduleItems;

public class DeleteScheduleItem
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id:int}", Handle)
            .WithName("DeleteScheduleItem");
    }

    public static Results<Ok, NotFound<string>> Handle(ScheduleItemService scheduleItemService, [FromRoute] int id)
    {
        var scheduleItem = scheduleItemService.GetScheduleItemByID(id);

        if (scheduleItem is not null)
        {
            scheduleItemService.DeleteScheduleItem(id);
            return TypedResults.Ok();
        }

        return TypedResults.NotFound($"A schedule item with the ID: {id} was not found.");
    }
}
