using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Schedules;

public class DeleteSchedule
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id:int}", Handle)
            .WithName("DeleteSchedule");
    }

    public static Results<Ok, NotFound<string>> Handle(ScheduleService scheduleService, [FromRoute] int id)
    {
        var schedule = scheduleService.GetScheduleByID(id);

        if (schedule is not null)
        {
            scheduleService.DeleteSchedule(id);
            return TypedResults.Ok();
        }

        return TypedResults.NotFound($"A schedule with the ID: {id} was not found.");
    }
}
