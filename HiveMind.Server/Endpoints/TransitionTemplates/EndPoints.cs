namespace HiveMind.Server.Endpoints.TransitionTemplates;

public class EndPoints
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("Transitions");
        Get.Map(endpoints);
        GetAll.Map(endpoints);
        Update.Map(endpoints);
    }
}