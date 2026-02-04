
namespace HiveMind.Server.Endpoints.ScheduleItems;

public class EndPoints
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("ScheduleItems");
        GetAllScheduleItems.Map(endpoints);
        GetScheduleItemById.Map(endpoints);
        GetScheduleItemsByScheduleId.Map(endpoints);
        CreateScheduleItem.Map(endpoints);
        UpdateScheduleItem.Map(endpoints);
        DeleteScheduleItem.Map(endpoints);
    }
}
