namespace HiveMind.Server.Endpoints.Shows;

public class EndPoints
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("Shows");
        GetShows.Map(endpoints);
    }
}

