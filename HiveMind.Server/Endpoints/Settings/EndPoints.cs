namespace HiveMind.Server.Endpoints.Settings;

public class EndPoints
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("Settings");
        GetSettings.Map(endpoints);
        UpdateSettings.Map(endpoints);
    }
}
