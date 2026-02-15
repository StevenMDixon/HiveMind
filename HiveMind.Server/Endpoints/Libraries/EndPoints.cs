
namespace HiveMind.Server.Endpoints.Libraries;

public class EndPoints
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("Libraries");
        GetAllLibraries.Map(endpoints);
        CreateLibrary.Map(endpoints);
        DeleteLibrary.Map(endpoints);
        GetLibraryById.Map(endpoints);
    }
}
