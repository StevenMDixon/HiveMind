using System.Reflection.Metadata;
using Microsoft.AspNetCore.Http.HttpResults;
using HiveMind.Server.Domain.Enums;

namespace HiveMind.Server.Endpoints;

public class QuerySettings
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("/querysettings");
        endpoints.MapGet("/", Handle).WithName("GetQuerySettings");
    }

    public record QueryEnumResults(List<QueryEnums.QueryOptions> Options);

    public static Results<Ok<QueryEnumResults>, NotFound> Handle()
    {
        var options = QueryEnums.GetAllowedOptions();
        return TypedResults.Ok<QueryEnumResults>(new QueryEnumResults(options));
    }
}
