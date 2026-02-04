namespace HiveMind.Server.Endpoints.CollectionScheduleItems;

public static class Endpoints
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("CollectionScheduleItems");
        GetAllCollectionScheduleItems.Map(endpoints);
        GetCollectionScheduleItemById.Map(endpoints);
        GetItemsByScheduleID.Map(endpoints);
        CreateCollectionScheduleItem.Map(endpoints);
        UpdateCollectionScheduleItem.Map(endpoints);
        DeleteCollectionScheduleItem.Map(endpoints);
    }
}