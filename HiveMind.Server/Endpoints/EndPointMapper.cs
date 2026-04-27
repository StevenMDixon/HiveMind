namespace HiveMind.Server.Endpoints;

public static class EndPointMapper
{
    public static void Map(IEndpointRouteBuilder builder)
    {
        Channels.EndPoints.Map(builder);
        Libraries.EndPoints.Map(builder);
        MediaItems.EndPoints.Map(builder);
        Lineup.EndPoints.Map(builder);
        Queries.EndPoints.Map(builder);
        QueryLineupItems.EndPoints.Map(builder);
        LineupItems.EndPoints.Map(builder);
        QuerySettings.Map(builder);
        QueryOptions.Map(builder);
        GetPlayoutTypes.Map(builder);
        GetPadTo.Map(builder);
        Shows.EndPoints.Map(builder);
    }
}
