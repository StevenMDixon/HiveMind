
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

    public record CollectionScheduleItem(int CollectionScheduleItemId, int CollectionId, int ScheduleItemId, int PlayDuration, int PlayCount, int PadTo, string CollectionType, string PlayoutType, int Index);

    public record ScheduleItem(int ScheduleItemId, int Index, string Type, string Name, int ScheduleId, ICollection<CollectionScheduleItem> Collections);

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
                schedule.ScheduleItems?.Select(item => new ScheduleItem(item.ScheduleItemId, item.Index, item.Type, item.Name, item.ScheduleId, item.Collections?.Select(c => new CollectionScheduleItem(c.CollectionScheduleItemId, c.CollectionId, c.ScheduleItemId, c.PlayDuration, c.PlayCount, c.PadTo, c.CollectionType.ToString(), c.PlayoutType.ToString(), c.Index)).ToList() ?? new List<CollectionScheduleItem>())).ToList() ?? new List<ScheduleItem>()));
        }

        return TypedResults.NotFound($"A schedule with the ID: {id} was not found.");
    }
}
