using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Collections;

public static class GetCollectionById
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:int}", Handle).WithName("GetCollectionById");
    }

    public record Collection(int CollectionID, string Query);

    public static Results<Ok<Collection>, NotFound<string>> Handle(CollectionService collectionService, [FromRoute] int id)
    {
        var collection = collectionService.GetCollectionByID(id);

        if (collection is not null)
        {
            return TypedResults.Ok(new Collection(collection.CollectionId, collection.Query));
        }

        return TypedResults.NotFound($"A collection with the ID: {id} was not found.");
    }
}
