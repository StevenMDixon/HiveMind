using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Collections;

public class DeleteCollection
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id:int}", Handle)
            .WithName("DeleteCollection");
    }

    public static Results<Ok, NotFound<string>> Handle(CollectionService collectionService, [FromRoute] int id)
    {
        var collection = collectionService.GetCollectionByID(id);

        if (collection is not null)
        {
            collectionService.Delete(id);
            return TypedResults.Ok();
        }

        return TypedResults.NotFound($"A collection with the ID: {id} was not found.");
    }
}
