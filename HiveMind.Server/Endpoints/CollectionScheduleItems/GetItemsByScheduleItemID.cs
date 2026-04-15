using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.CollectionScheduleItems;

public class GetItemsByScheduleItemID
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/scheduleItem/{id:int}", Handle).WithName("GetItemsByScheduleItemID");
    }

    public record CollectionScheduleItem(int CollectionScheduleItemId, int CollectionId, int ScheduleItemId, int PlayDuration, int PlayCount, int PadTo, CollectionType CollectionType, PlayoutType PlayoutType, int Index);
    public record ScheduleItemResponse(List<CollectionScheduleItem> Items);

    public static Results<Ok<ScheduleItemResponse>, NotFound<string>> Handle(CollectionSchedulteItemService scheduleItemService, [FromRoute] int id)
    {
        var collectionScheduleItems = scheduleItemService.GetCollectionScheduleItemsByScheduleItemID(id);

        if (collectionScheduleItems is not null)
        {
            return TypedResults.Ok(new ScheduleItemResponse(collectionScheduleItems.Select(x => new CollectionScheduleItem(x.CollectionScheduleItemId, x.CollectionId, x.ScheduleItemId, x.PlayDuration, x.PlayCount, x.PadTo, x.CollectionType, x.PlayoutType, x.Index)).ToList()));
        }

        return TypedResults.NotFound($"A collection with the ID: {id} was not found.");
    }
}
