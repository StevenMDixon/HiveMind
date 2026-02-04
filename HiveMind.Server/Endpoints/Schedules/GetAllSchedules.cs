using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HiveMind.Server.Endpoints.Schedules;

public static class GetAllSchedules
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", Handle).WithName("GetAllSchedules");
    }

    public record Schedule(int ScheduleId, string ScheduleName, int ChannelId, TimeOnly StartTime);
    public record GetAllSchedulesResponse(List<Schedule> Schedules);

    public static Results<Ok<GetAllSchedulesResponse>, NotFound> Handle(ScheduleService scheduleService)
    {
        var schedules = scheduleService.GetAllSchedules();

        return TypedResults.Ok(new GetAllSchedulesResponse(schedules.Select(x => new Schedule(x.ScheduleId, x.ScheduleName, x.ChannelId, x.StartTime)).ToList()));
    }
}
