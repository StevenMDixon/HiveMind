using FluentValidation;
using HiveMind.Server.Entities;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Collections;

public class UpdateCollection
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("/{id:int}", Handle)
            .WithRequestValidation<CollectionRequest>()
            .WithName("UpdateCollection")
            .ProducesValidationProblem();
    }

    public class Validator : AbstractValidator<CollectionRequest>
    {
        public Validator()
        {
            RuleFor(x => x.CollectionName).NotEmpty();
        }
    }


    public record CollectionRequest(string CollectionName, ICollection<QueryFilters> Filters);

    public static Results<Ok, NotFound<string>, ValidationProblem> Handle(CollectionService collectionService, [FromRoute] int id, [FromBody] CollectionRequest request)
    {
        var collection = collectionService.GetCollectionByID(id);

        if (collection is not null)
        {
            collection.Name = request.CollectionName;
            collection.Filters = request.Filters;
            foreach (var item in collection.Filters)
            {
                if (item.QueryFilterId < 0) item.QueryFilterId = 0;
            }
            collectionService.Update(collection);
            return TypedResults.Ok();
        }

        return TypedResults.NotFound($"A collection with the ID: {id} was not found.");
    }
}
