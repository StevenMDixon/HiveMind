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

    public record Filter(int QueryFilterID, string Field, string Operator, string Value);

    public record Collection(int CollectionID, string CollectionName, ICollection<Filter>? Filters);

    public static Results<Ok<Collection>, NotFound<string>> Handle(CollectionService collectionService, [FromRoute] int id)
    {
        var collection = collectionService.GetCollectionByID(id);

        if (collection is not null)
        {
            return TypedResults.Ok(new Collection(collection.CollectionId, collection.Name, collection.Filters?.Select(x => new Filter(x.QueryFilterId, x.Field, x.Operator, x.Value)).ToList()));
        }

        return TypedResults.NotFound($"A collection with the ID: {id} was not found.");
    }
}
