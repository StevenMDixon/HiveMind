
namespace HiveMind.Server.Endpoints.TaggingQueue;

public class EndPoints
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("TagQueue");

        QueuePercentages.Map(endpoints);
        GetTagCategories.Map(endpoints);
    }
}
