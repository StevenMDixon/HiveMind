namespace HiveMind.Server.Endpoints.Lineup;

public class EndPoints
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("Lineups");
        GetAllLineups.Map(endpoints);
        GetLineupById.Map(endpoints);
        CreateLineup.Map(endpoints);
        UpdateLineup.Map(endpoints);
        DeleteLineup.Map(endpoints);
    }
}
