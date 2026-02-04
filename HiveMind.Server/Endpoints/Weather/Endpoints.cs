namespace HiveMind.Server.Endpoints.Weather;

public class Endpoints
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("/weatherforecast");
        GetWeather.Map(endpoints);
    }
}
