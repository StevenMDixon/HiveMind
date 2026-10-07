
namespace HiveMind.Server.Endpoints.Scheduler;

public class EndPoints
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("/scheduler");

        GetLineupResults.Map(endpoints);
    }
}
