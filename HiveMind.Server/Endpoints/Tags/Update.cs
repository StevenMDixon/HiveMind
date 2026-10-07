using FluentValidation;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Tags;

public class Update
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("/{int:id}", Handle)
            .WithRequestValidation<UpdateRequest>()
            .WithName("Update");
    }

    public class Validator : AbstractValidator<UpdateRequest>
    {
        public Validator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }


    public record UpdateRequest(string Name);

    public static Results<Ok, NoContent, NotFound<string>, ValidationProblem> Handle(TagsService service, [FromBody] UpdateRequest updateRequest, [FromRoute] int id)
    {
        var tagToUpdate = service.GetById(id);

        if (tagToUpdate == null) return TypedResults.NotFound($"A Tag with the ID: {id} was not found.");

        var existingTag = service.GetByName(updateRequest.Name);

        if (existingTag != null)
            return TypedResults.ValidationProblem(new Dictionary<string, string[]>
            {
                [nameof(updateRequest.Name)] = new[] { $"A Tag with the Name: {updateRequest.Name} already exists" }
            });
        
        tagToUpdate.Name = updateRequest.Name;

        service.Update(tagToUpdate);

        return TypedResults.NoContent();
    }
}
