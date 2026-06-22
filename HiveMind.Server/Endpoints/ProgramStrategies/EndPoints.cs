
namespace HiveMind.Server.Endpoints.ProgramStrategies;

public class EndPoints
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("strategies");
        GetAll.Map(endpoints);
        GetById.Map(endpoints);
        Update.Map(endpoints);
        Delete.Map(endpoints);
        Create.Map(endpoints);
    }
}
