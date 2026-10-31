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

    public record Tag(int Id, string Name);

    public record Response(List<Tag> Tags);

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

        return TypedResults.Ok(new Response(tags.Select(x => new Tag(x.Id, x.Name)).ToList()));
    }
}
