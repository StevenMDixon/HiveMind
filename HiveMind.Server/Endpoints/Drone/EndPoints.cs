
namespace HiveMind.Server.Endpoints.Drone;

public class EndPoints
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("Drone");

        GetScheduleData.Map(endpoints);
        Create.Map(endpoints);
        Get.Map(endpoints);
    }
}
