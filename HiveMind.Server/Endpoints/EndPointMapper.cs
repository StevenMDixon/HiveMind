namespace HiveMind.Server.Endpoints;

public static class EndPointMapper
{
    public static void Map(IEndpointRouteBuilder builder)
    {
        var api = builder.MapGroup("/Api");
        Stations.EndPoints.Map(api);
        Libraries.EndPoints.Map(api);
        MediaItems.EndPoints.Map(api);
        Lineup.EndPoints.Map(api);
        Queries.EndPoints.Map(api);
        QueryLineupItems.EndPoints.Map(api);
        LineupItems.EndPoints.Map(api);
        Enums.EndPoints.Map(api);
        Shows.EndPoints.Map(api);
        Settings.EndPoints.Map(api);
        ProgramStrategies.EndPoints.Map(api);
    }
}
