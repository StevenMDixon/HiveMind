namespace HiveMind.Server.Endpoints.QueryLineupItems;

public static class EndPoints
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("QueryLineupItems");
        GetQueryLineupItems.Map(endpoints);
        GetQueryLineupItemById.Map(endpoints);
        GetItemsByLineupItemID.Map(endpoints);
        CreateQueryLineupItem.Map(endpoints);
        UpdateQueryLineupItem.Map(endpoints);
        DeleteQueryLineupItem.Map(endpoints);
    }
}