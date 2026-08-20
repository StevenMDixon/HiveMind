namespace HiveMind.Server.Endpoints.Enums;

public class EndPoints
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("Enums");
        GetPlayoutTypes.Map(endpoints);
        QuerySettings.Map(endpoints);
        QueryOptions.Map(endpoints);
        GetLineupSelectionTypes.Map(endpoints);
    }
}
