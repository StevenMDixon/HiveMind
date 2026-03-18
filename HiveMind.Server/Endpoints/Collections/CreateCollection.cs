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
            .WithRequestValidation<CollectionRequest>()
            .WithName("CreateCollection")
            .ProducesValidationProblem();
    }

    public class Validator : AbstractValidator<CollectionRequest>
    {
        public Validator()
        {
            RuleFor(x => x.CollectionName).NotEmpty();
        }
    }

    public record Response(int CollectionId);

    public record CollectionRequest(string CollectionName);
    public static Results<Ok<Response>, NoContent, ValidationProblem> Handle(CollectionService collectionService, [FromBody] CollectionRequest request)
    {
        var newCollection = new Entities.Collection
        {
            Name = request.CollectionName
        };

        newCollection = collectionService.AddCollection(newCollection);

        return TypedResults.Ok(new Response(newCollection.CollectionId));
    }
}
