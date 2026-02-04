namespace HiveMind.Server.Endpoints;

public static class EndPointMapper
{
    public static void Map(IEndpointRouteBuilder builder)
    {
        Channels.Endpoints.Map(builder);
        Weather.Endpoints.Map(builder);
    }
}
