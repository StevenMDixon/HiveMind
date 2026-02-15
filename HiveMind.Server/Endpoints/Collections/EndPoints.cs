
namespace HiveMind.Server.Endpoints.Collections;

public static class Endpoints
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("collections");
        GetAllCollections.Map(endpoints);
        GetCollectionById.Map(endpoints);
        CreateCollection.Map(endpoints);
        UpdateCollection.Map(endpoints);
        DeleteCollection.Map(endpoints);
    }
}