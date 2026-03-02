using HiveMind.Server.Entities;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HiveMind.Server.Endpoints.Collections;

public static class GetAllCollections
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", Handle);
    }

    public record Collection(int CollectionID, string CollectionName);
    public record GetAllCollectionsResponse(List<Collection> Collections);

    public static Results<Ok<GetAllCollectionsResponse>, NotFound> Handle(CollectionService collectionService)
    {
        var collections = collectionService.GetAllCollections();

        return TypedResults.Ok(new GetAllCollectionsResponse(collections.Select(x => new Collection(x.CollectionId, x.Name)).ToList()));
    }
}
