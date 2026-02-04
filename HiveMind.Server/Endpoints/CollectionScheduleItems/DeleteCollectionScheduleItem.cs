using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.CollectionScheduleItems;

public class DeleteCollectionScheduleItem
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id:int}", Handle)
            .WithName("DeleteCollectionScheduleItem");
    }

    public static Results<Ok, NotFound<string>> Handle(CollectionSchedulteItemService scheduleItemService, [FromRoute] int id)
    {
        var item = scheduleItemService.GetCollectionScheduleItemByID(id);

        if (item is not null)
        {
            scheduleItemService.Delete(id);
            return TypedResults.Ok();
        }

        return TypedResults.NotFound($"A collection schedule item with the ID: {id} was not found.");
    }
}
