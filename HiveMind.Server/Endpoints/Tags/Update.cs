using FluentValidation;
using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Tags;

public class Update
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("/{id:int}", Handle)
            .WithRequestValidation<UpdateRequest>()
            .WithName("UpdateTag");
    }

    public class Validator : AbstractValidator<UpdateRequest>
    {
        public Validator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Category).IsInEnum();
        }
    }


    public record UpdateRequest(string Name, TagCategory Category);

    public static Results<Ok, NoContent, NotFound<string>, ValidationProblem> Handle(TagsService service, [FromBody] UpdateRequest updateRequest, [FromRoute] int id)
    {
        var tagToUpdate = service.GetById(id);

        if (tagToUpdate == null) return TypedResults.NotFound($"A Tag with the ID: {id} was not found.");

        var existingTag = service.GetByName(updateRequest.Name);

        if (existingTag != null && existingTag.Id != id)
            return TypedResults.ValidationProblem(new Dictionary<string, string[]>
            {
                [nameof(updateRequest.Name)] = new[] { $"A Tag with the Name: {updateRequest.Name} already exists" }
            });
        
        tagToUpdate.Name = updateRequest.Name;
        tagToUpdate.Category = updateRequest.Category;

        service.Update(tagToUpdate);

        return TypedResults.NoContent();
    }
}
