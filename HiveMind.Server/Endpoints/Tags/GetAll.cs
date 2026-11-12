using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HiveMind.Server.Endpoints.Tags;

public class GetAll
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", Handle)
            .WithName("GetAllTags");
    }

    public record Tag(int Id, string Name, TagCategory Category);

    public record Response(List<Tag> Tags, int CurrentPage, int PageSize, int DataCount);

    public record Parameters(
       string? Search,
       int? Page,
       int? Count
    );

    public static Results<Ok<Response>, NoContent, ValidationProblem> Handle(TagsService service, [AsParameters] Parameters filters)
    {
        var tags = service.GetAllTags();

        var pageSize = filters.Count ?? 100;
        var currentPage = filters.Page ?? 1;
        var dataCount = tags.Count();

        if (filters.Search != null && filters.Search != string.Empty) tags = tags.Where(x => x.Name.ToLower().Contains(filters.Search.ToLower()));

        var m = (pageSize * currentPage);
        tags = tags.Skip((pageSize * (currentPage - 1))).Take(pageSize);

        return TypedResults.Ok(new Response(tags.Select(x => new Tag(x.Id, x.Name, x.Category)).ToList(), currentPage, pageSize, dataCount));
    }
}
