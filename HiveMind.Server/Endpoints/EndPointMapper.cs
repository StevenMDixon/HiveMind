namespace HiveMind.Server.Endpoints;

public static class EndPointMapper
{
    public static void Map(IEndpointRouteBuilder builder)
    {
        Channels.EndPoints.Map(builder);
        Libraries.EndPoints.Map(builder);
        Weather.EndPoints.Map(builder);
        Collections.Endpoints.Map(builder);
        MediaItems.EndPoints.Map(builder);
        Schedules.EndPoints.Map(builder);
        QuerySettings.Map(builder);
    }
}
