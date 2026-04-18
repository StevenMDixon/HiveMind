using HiveMind.Server.Domain.Enums;
using Microsoft.AspNetCore.Http.HttpResults;
using static HiveMind.Server.Domain.Enums.QueryEnums;

namespace HiveMind.Server.Endpoints;
    public class QueryOptions
    {
    public static void Map(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("/queryoptions");
        endpoints.MapGet("/", Handle).WithName("GetQueryOptions");
    }

    public record QueryOptionItem(int id, string name);

    public record QueryOptionsEnumResults(ICollection<QueryOptionItem> options);

    public static Results<Ok<QueryOptionsEnumResults>, NotFound> Handle()
    {
        var options = Enum.GetValues<QueryAllowedOperators>();

        return TypedResults.Ok(
            new QueryOptionsEnumResults(
                options.Select(x => new QueryOptionItem((int)x, x.ToString())).ToList()
            ));
    }
}

