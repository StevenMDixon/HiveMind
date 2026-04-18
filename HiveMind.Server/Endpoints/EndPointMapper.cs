namespace HiveMind.Server.Endpoints;

public static class EndPointMapper
{
    public static void Map(IEndpointRouteBuilder builder)
    {
        Channels.EndPoints.Map(builder);
        Libraries.EndPoints.Map(builder);
        Queries.EndPoints.Map(builder);
        MediaItems.EndPoints.Map(builder);
        Schedules.EndPoints.Map(builder);
        ScheduleItems.EndPoints.Map(builder);
        QueryScheduleItems.Endpoints.Map(builder);
        QuerySettings.Map(builder);
        QueryOptions.Map(builder);
        GetPlayoutTypes.Map(builder);
    }
}
