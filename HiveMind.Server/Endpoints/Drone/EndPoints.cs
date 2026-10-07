
namespace HiveMind.Server.Endpoints.Drone;

public class EndPoints
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("Drone");

        GetScheduleData.Map(endpoints);
        Create.Map(endpoints);
        GetAll.Map(endpoints);
        Update.Map(endpoints);
        Delete.Map(endpoints);
        RegisterDrone.Map(endpoints);
    }
}
