namespace HiveMind.Server.Endpoints.Stations;

public static class EndPoints
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("stations");
        GetAll.Map(endpoints);
        Create.Map(endpoints);
        Update.Map(endpoints);
        Delete.Map(endpoints);
        Get.Map(endpoints);
    }
}
