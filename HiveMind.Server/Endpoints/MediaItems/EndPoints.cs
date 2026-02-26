namespace HiveMind.Server.Endpoints.MediaItems;

public class EndPoints
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("MediaItems");
        GetAllMediaItems.Map(endpoints);
    }
}

