
namespace HiveMind.Server.Endpoints.LineupItems;

public class EndPoints
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("lineupitems");
        GetAllLineupItems.Map(endpoints);
        GetLineupItemById.Map(endpoints);
        GetItemsByLineupId.Map(endpoints);
        CreateLineupItem.Map(endpoints);
        UpdateLineupItem.Map(endpoints);
        DeleteLineupItem.Map(endpoints);
    }
}
