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

    public record CollectionScheduleItem(int CollectionScheduleItemId, int CollectionId, int ScheduleItemId, int Duration, string CollectionType, string PlayoutType, int Index, bool DisableIntroBump, bool DisableInterStitials);

    public static Results<Ok<CollectionScheduleItem>, NotFound<string>> Handle(CollectionSchedulteItemService scheduleItemService, [FromRoute] int id)
    {
        var item = scheduleItemService.GetCollectionScheduleItemByID(id);

        if (item is not null)
        {
            return TypedResults.Ok(new CollectionScheduleItem(item.CollectionScheduleItemId, item.CollectionId, item.ScheduleItemId, item.Duration, item.CollectionType.ToString(), item.PlayoutType.ToString(), item.Index, item.DisableIntroBump, item.DisableInterStitials));
        }

        return TypedResults.NotFound($"A collection schedule item with the ID: {id} was not found.");
    }
}
