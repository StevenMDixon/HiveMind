
namespace HiveMind.Server.Endpoints.Queries;

public static class EndPoints
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("queries");
        GetAll.Map(endpoints);
        Get.Map(endpoints);
        GetQueryTypes.Map(endpoints);
        Create.Map(endpoints);
        Update.Map(endpoints);
        Delete.Map(endpoints);
        QueryTest.Map(endpoints);
    }
}