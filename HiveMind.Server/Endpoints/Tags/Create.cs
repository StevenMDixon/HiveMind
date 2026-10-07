using FluentValidation;
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
            .WithName("Create");
    }

    public class Validator : AbstractValidator<CreateRequest>
    {
        public Validator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }

    public record CreateRequest(string Name);

    public static Results<Ok<int>, NoContent, ValidationProblem> Handle(TagsService service, [FromBody] CreateRequest createRequest)
    {
        var existingTag = service.GetByName(createRequest.Name);

        if (existingTag != null) return TypedResults.Ok(existingTag.Id);

        var newTag = service.Create(new Entities.Tags() { Name = createRequest.Name });

        return TypedResults.Ok(newTag.Id);
    }
}
