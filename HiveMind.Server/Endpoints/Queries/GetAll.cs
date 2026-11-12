using HiveMind.Server.Entities;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HiveMind.Server.Endpoints.Queries;

public static class GetAll
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", Handle);
    }

    public record Query(int Id, string Name, string Type);
    public record GetAllQueriesResponse(List<Query> Queries, int CurrentPage, int PageSize, int DataCount);

    public record Parameters(
       string? Search,
       int? Page,
       int? Count
    );

    public static Results<Ok<GetAllQueriesResponse>, NotFound> Handle(QueryService queryService, [AsParameters] Parameters filters)
    {
        var queries = queryService.GetAllQueries();

        var pageSize = filters.Count ?? 100;
        var currentPage = filters.Page ?? 1;
        var dataCount = queries.Count();

        if (filters.Search != null && filters.Search != string.Empty) queries = queries.Where(x => x.Name.ToLower().Contains(filters.Search.ToLower()));

        var m = (pageSize * currentPage);
        queries = queries.Skip((pageSize * (currentPage - 1))).Take(pageSize);

        return TypedResults.Ok(new GetAllQueriesResponse(queries.Select(x => new Query(x.Id, x.Name, x.Type.ToString())).ToList(), currentPage, pageSize, dataCount));
    }
}
