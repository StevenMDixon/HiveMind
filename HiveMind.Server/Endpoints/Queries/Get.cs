using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Queries;

public static class Get
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:int}", Handle).WithName("GetQueryById");
    }

    public record Filter(int Id, int Field, int Operator, string Value);

    public record Query(int Id, string Name, ICollection<Filter>? Filters);

    public static Results<Ok<Query>, NotFound<string>> Handle(QueryService queryService, [FromRoute] int id)
    {
        var query = queryService.GetQueryByID(id);
        if (query is not null)
        {
            return TypedResults.Ok(new Query(query.Id, query.Name, query.Filters?.Select(x => new Filter(x.Id, (int)x.Field, (int)x.Operator, x.Value)).ToList()));
        }

        return TypedResults.NotFound($"A query with the ID: {id} was not found.");
    }
}
