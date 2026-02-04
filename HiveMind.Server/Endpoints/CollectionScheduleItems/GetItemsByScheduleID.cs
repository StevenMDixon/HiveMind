using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.CollectionScheduleItems;

public class GetItemsByScheduleID
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/schedule/{id:int}", Handle).WithName("GetItemsByScheduleID");
    }

    public record CollectionScheduleItem(int CollectionScheduleItemId, int CollectionId, int ScheduleItemId, int Duration, string CollectionType, string PlayoutType, int Index, bool DisableIntroBump, bool DisableInterStitials);
    public record ScheduleItemResponse(List<CollectionScheduleItem> Items);

    public static Results<Ok<ScheduleItemResponse>, NotFound<string>> Handle(CollectionSchedulteItemService scheduleItemService, [FromRoute] int id)
    {
        var collectionScheduleItems = scheduleItemService.GetCollectionScheduleItemsByScheduleItemID(id);

        if (collectionScheduleItems is not null)
        {
            return TypedResults.Ok(new ScheduleItemResponse(collectionScheduleItems.Select(x => new CollectionScheduleItem(x.CollectionScheduleItemId, x.CollectionId, x.ScheduleItemId, x.Duration, x.CollectionType.ToString(), x.PlayoutType.ToString(), x.Index, x.DisableIntroBump, x.DisableInterStitials)).ToList()));
        }

        return TypedResults.NotFound($"A collection with the ID: {id} was not found.");
    }
}
