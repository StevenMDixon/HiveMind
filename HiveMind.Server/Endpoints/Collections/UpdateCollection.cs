using FluentValidation;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Collections;

public class UpdateCollection
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("/{id:int}", Handle)
            .WithRequestValidation<Validator>()
            .WithName("UpdateCollection")
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

    public static Results<Ok, NotFound<string>, ValidationProblem> Handle(CollectionService collectionService, [FromRoute] int id, [FromBody] CollectionRequest request)
    {
        var collection = collectionService.GetCollectionByID(id);

        if (collection is not null)
        {
            collection.Query = request.Query;
            collectionService.Update(collection);
            return TypedResults.Ok();
        }

        return TypedResults.NotFound($"A collection with the ID: {id} was not found.");
    }
}
