using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Schedules;

public static class GetScheduleById
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:int}", Handle).WithName("GetScheduleById");
    }

    public record Schedule(int ScheduleId, string ScheduleName, int ChannelId, TimeOnly StartTime);

    public static Results<Ok<Schedule>, NotFound<string>> Handle(ScheduleService scheduleService, [FromRoute] int id)
    {
        var schedule = scheduleService.GetScheduleByID(id);

        if (schedule is not null)
        {
            return TypedResults.Ok(new Schedule(schedule.ScheduleId, schedule.ScheduleName, schedule.ChannelId, schedule.StartTime));
        }

        return TypedResults.NotFound($"A schedule with the ID: {id} was not found.");
    }
}
