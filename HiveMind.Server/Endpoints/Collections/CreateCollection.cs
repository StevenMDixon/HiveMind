using FluentValidation;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Collections;

public class CreateCollection
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", Handle)
            .WithRequestValidation<Validator>()
            .WithName("CreateCollection")
            .ProducesValidationProblem();
    }

    public class Validator : AbstractValidator<CollectionRequest>
    {
        public Validator()
        {
            RuleFor(x => x.Query).NotEmpty();
        }
    }

    public record CollectionRequest(string Query);

    public static Results<Ok, NoContent, ValidationProblem> Handle(CollectionService collectionService, [FromBody] CollectionRequest request)
    {
        var newCollection = new Entities.Collection
        {
            Query = request.Query
        };

        collectionService.AddCollection(newCollection);

        return TypedResults.NoContent();
    }
}
