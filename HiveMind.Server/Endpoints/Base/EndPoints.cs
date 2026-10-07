
namespace HiveMind.Server.Endpoints.Base;

public class EndPoints
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("");

        Get.Map(endpoints);
        Create.Map(endpoints);
        Update.Map(endpoints);
        Delete.Map(endpoints);
    }
}
