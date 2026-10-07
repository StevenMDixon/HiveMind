
namespace HiveMind.Server.Endpoints.Tags;

public class EndPoints
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("Tags");

        GetAll.Map(endpoints);
        Create.Map(endpoints);
        Update.Map(endpoints);
        Delete.Map(endpoints);
    }
}
