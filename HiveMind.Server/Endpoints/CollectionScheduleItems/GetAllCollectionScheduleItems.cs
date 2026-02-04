using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.CollectionScheduleItems;

public class GetAllCollectionScheduleItems
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", Handle);
    }

    public record CollectionScheduleItem(int CollectionScheduleItemId, int CollectionId, int ScheduleItemId, int Duration, string CollectionType, string PlayoutType, int Index, bool DisableIntroBump, bool DisableInterStitials);
    public record ScheduleItemResponse(List<CollectionScheduleItem> Items);

    public static Results<Ok<ScheduleItemResponse>, NotFound<string>> Handle(CollectionSchedulteItemService scheduleItemService)
    {
        var collectionScheduleItems = scheduleItemService.GetAllCollectionScheduleItems();
       
        return TypedResults.Ok(new ScheduleItemResponse(collectionScheduleItems.Select(x => new CollectionScheduleItem(x.CollectionScheduleItemId, x.CollectionId, x.ScheduleItemId, x.Duration, x.CollectionType.ToString(), x.PlayoutType.ToString(), x.Index, x.DisableIntroBump, x.DisableInterStitials)).ToList()));
    }
}
