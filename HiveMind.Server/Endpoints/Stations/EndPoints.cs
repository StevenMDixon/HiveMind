namespace HiveMind.Server.Endpoints.Stations;

public static class EndPoints
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("stations");
        GetAllStations.Map(endpoints);
        CreateStation.Map(endpoints);
        UpdateStation.Map(endpoints);
        DeleteStation.Map(endpoints);
        GetStation.Map(endpoints);
    }
}
