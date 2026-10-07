namespace HiveMind.Server.Endpoints.MediaItems;

public class EndPoints
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("MediaItems");
        Get.Map(endpoints);
        GetAll.Map(endpoints);
        Update.Map(endpoints);
    }
}

