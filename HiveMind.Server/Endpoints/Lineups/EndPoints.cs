namespace HiveMind.Server.Endpoints.Lineup;

public class EndPoints
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("Lineups");
        GetAll.Map(endpoints);
        Get.Map(endpoints);
        Create.Map(endpoints);
        Update.Map(endpoints);
        Delete.Map(endpoints);
    }
}
