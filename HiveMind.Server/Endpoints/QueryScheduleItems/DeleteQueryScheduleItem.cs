using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.QueryScheduleItems;

public class DeleteQueryScheduleItem
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id:int}", Handle)
            .WithName("DeleteQueryScheduleItem");
    }

    public static Results<Ok, NotFound<string>> Handle(QueryScheduleItemService queryScheduleItemService, [FromRoute] int id)
    {
        var item = queryScheduleItemService.GetQueryScheduleItemByID(id);

        if (item is not null)
        {
            queryScheduleItemService.Delete(id);
            return TypedResults.Ok();
        }

        return TypedResults.NotFound($"A query schedule item with the ID: {id} was not found.");
    }
}
