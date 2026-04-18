
using HiveMind.Server.Domain.Enums;
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

    public record QueryScheduleItem(int QueryScheduleItemId, int QueryId, int ScheduleItemId, int PlayDuration, int PlayCount, int PadTo, QueryType QueryType, PlayoutType PlayoutType, int Index);

    public record ScheduleItem(int ScheduleItemId, int Index, string Type, string Name, int ScheduleId, ICollection<QueryScheduleItem> Queries);

    public record Schedule(int ScheduleId, string ScheduleName, int? ChannelId, TimeOnly StartTime, ICollection<ScheduleItem> ScheduleItems);

    public static Results<Ok<Schedule>, NotFound<string>> Handle(ScheduleService scheduleService, [FromRoute] int id)
    {
        var schedule = scheduleService.GetScheduleByID(id);

        if (schedule is not null)
        {
            return TypedResults.Ok(new Schedule(
                schedule.ScheduleId, 
                schedule.ScheduleName, 
                schedule.ChannelId, 
                schedule.StartTime, 
                schedule.ScheduleItems?.Select(item => new ScheduleItem(item.ScheduleItemId, item.Index, item.Type, item.Name, item.ScheduleId, item.Queries?.Select(c => new QueryScheduleItem(c.QueryScheduleItemId, c.QueryId, c.ScheduleItemId, c.PlayDuration, c.PlayCount, c.PadTo, c.QueryType, c.PlayoutType, c.Index)).ToList() ?? new List<QueryScheduleItem>())).ToList() ?? new List<ScheduleItem>()));
        }

        return TypedResults.NotFound($"A schedule with the ID: {id} was not found.");
    }
}
