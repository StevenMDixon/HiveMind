using FluentValidation;
using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Tags;

public class Create
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", Handle)
            .WithRequestValidation<CreateRequest>()
            .WithName("CreateTags");
    }

    public class Validator : AbstractValidator<CreateRequest>
    {
        public Validator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Category).IsInEnum();
        }
    }

    public record CreateRequest(string Name, TagCategory Category);

    public static Results<Ok<int>, NoContent, ValidationProblem> Handle(TagsService service, [FromBody] CreateRequest createRequest)
    {
        var existingTag = service.GetByName(createRequest.Name);

        if (existingTag != null) return TypedResults.Ok(existingTag.Id);

        var newTag = service.Create(new Entities.Tags() { Name = createRequest.Name, Category = createRequest.Category });

        return TypedResults.Ok(newTag.Id);
    }
}
