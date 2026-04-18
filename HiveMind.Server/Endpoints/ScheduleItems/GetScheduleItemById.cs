using HiveMind.Server.Domain.Enums;
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

    public record QueryScheduleItem(int QueryScheduleItemId, int QueryId, int ScheduleItemId, int PlayDuration, int PlayCount, int PadTo, QueryType QueryType, PlayoutType PlayoutType, int Index);


    public record ScheduleItem(int ScheduleItemId, int Index, string name, string Type, int ScheduleId, ICollection<QueryScheduleItem>? Queries);

    public static Results<Ok<ScheduleItem>, NotFound<string>> Handle(ScheduleItemService scheduleItemService, [FromRoute] int id)
    {
        var scheduleItem = scheduleItemService.GetScheduleItemByID(id);

        if (scheduleItem is not null)
        {
            return TypedResults.Ok(new ScheduleItem(scheduleItem.ScheduleItemId, scheduleItem.Index, scheduleItem.Name, scheduleItem.Type, scheduleItem.ScheduleId, scheduleItem.Queries.Select(query => new QueryScheduleItem(query.QueryScheduleItemId, query.QueryId, query.ScheduleItemId, query.PlayDuration, query.PlayCount, query.PadTo, query.QueryType, query.PlayoutType, query.Index)).ToArray()));
        }

        return TypedResults.NotFound($"A schedule item with the ID: {id} was not found.");
    }
}
