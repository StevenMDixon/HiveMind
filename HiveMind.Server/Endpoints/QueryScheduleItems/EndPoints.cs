namespace HiveMind.Server.Endpoints.QueryScheduleItems;

public static class Endpoints
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("QueryScheduleItems");
        GetAllQueryScheduleItems.Map(endpoints);
        GetQueryScheduleItemById.Map(endpoints);
        GetItemsByScheduleItemID.Map(endpoints);
        CreateQueryScheduleItem.Map(endpoints);
        UpdateQueryScheduleItem.Map(endpoints);
        DeleteQueryScheduleItem.Map(endpoints);
    }
}