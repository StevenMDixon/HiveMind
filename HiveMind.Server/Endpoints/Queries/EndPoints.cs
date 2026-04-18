
namespace HiveMind.Server.Endpoints.Queries;

public static class EndPoints
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("queries");
        GetAllQueries.Map(endpoints);
        GetQueryById.Map(endpoints);
        GetQueryTypes.Map(endpoints);
        CreateQuery.Map(endpoints);
        UpdateQuery.Map(endpoints);
        DeleteQuery.Map(endpoints);
        QueryTest.Map(endpoints);
    }
}