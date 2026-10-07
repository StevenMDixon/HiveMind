using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Tags;

public class Delete
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id:int}", Handle)
            .WithName("Delete");
    }

    public static Results<Ok, NoContent, ValidationProblem, NotFound<string>> Handle(TagsService service, [FromRoute] int id)
    {
        var existingTag = service.GetById(id);

        if(existingTag == null) return TypedResults.NotFound($"A Tag with the ID: {id} was not found.");

        service.Delete(existingTag);

        return TypedResults.NoContent();
    }
}
