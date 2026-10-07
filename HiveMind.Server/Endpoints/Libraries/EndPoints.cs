
namespace HiveMind.Server.Endpoints.Libraries;

public class EndPoints
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("Libraries");
        GetAll.Map(endpoints);
        Create.Map(endpoints);
        Delete.Map(endpoints);
        Get.Map(endpoints);
        Update.Map(endpoints);
        GetLibraryTypes.Map(endpoints);
        ReprocessLibrary.Map(endpoints);
    }
}
