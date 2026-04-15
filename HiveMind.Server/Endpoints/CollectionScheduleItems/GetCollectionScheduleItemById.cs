using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.CollectionScheduleItems;

public static class GetCollectionScheduleItemById
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:int}", Handle).WithName("GetCollectionScheduleItemById");
    }

    public record CollectionScheduleItem(int CollectionScheduleItemId, int CollectionId, int ScheduleItemId, int PlayDuration, int PlayCount, int PadTo, CollectionType CollectionType, PlayoutType PlayoutType, int Index);

    public static Results<Ok<CollectionScheduleItem>, NotFound<string>> Handle(CollectionSchedulteItemService scheduleItemService, [FromRoute] int id)
    {
        var item = scheduleItemService.GetCollectionScheduleItemByID(id);

        if (item is not null)
        {
            return TypedResults.Ok(new CollectionScheduleItem(item.CollectionScheduleItemId, item.CollectionId, item.ScheduleItemId, item.PlayDuration, item.PlayCount, item.PadTo, item.CollectionType, item.PlayoutType, item.Index));
        }

        return TypedResults.NotFound($"A collection schedule item with the ID: {id} was not found.");
    }
}
