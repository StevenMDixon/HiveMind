using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HiveMind.Server.Endpoints.Tags;

public class GetAll
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", Handle)
            .WithName("GetAll");
    }

    public record Tag(int Id, string Name);

    public record Response(List<Tag> Tags);

    public static Results<Ok<Response>, NoContent, ValidationProblem> Handle(TagsService service)
    {
        var tags = service.GetAllTags();

        return TypedResults.Ok(new Response(tags.Select(x => new Tag(x.Id, x.Name)).ToList()));
    }
}
