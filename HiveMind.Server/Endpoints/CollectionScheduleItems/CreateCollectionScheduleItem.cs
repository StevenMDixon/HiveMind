using FluentValidation;
using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.CollectionScheduleItems;

public class CreateCollectionScheduleItem
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", Handle)
            .WithRequestValidation<Validator>()
            .WithName("CreateCollectionScheduleItem")
            .ProducesValidationProblem();
    }

    public class Validator : AbstractValidator<CollectionScheduleItemRequest>
    {
        public Validator()
        {
            RuleFor(x => x.CollectionId).GreaterThan(0);
            RuleFor(x => x.ScheduleItemId).GreaterThan(0);
            RuleFor(x => x.Duration).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Index).GreaterThanOrEqualTo(0);
            RuleFor(x => x.CollectionType).IsInEnum();
            RuleFor(x => x.PlayoutType).IsInEnum();
        }
    }

    public record CollectionScheduleItemRequest(int CollectionId, int ScheduleItemId, int Duration, CollectionType CollectionType, PlayoutType PlayoutType, int Index, bool DisableIntroBump, bool DisableInterStitials);

    public static Results<Ok, NoContent, ValidationProblem> Handle(CollectionSchedulteItemService scheduleItemService, [FromBody] CollectionScheduleItemRequest request)
    {
        var newItem = new Entities.CollectionScheduleItem
        {
            CollectionId = request.CollectionId,
            ScheduleItemId = request.ScheduleItemId,
            Duration = request.Duration,
            CollectionType = request.CollectionType,
            PlayoutType = request.PlayoutType,
            Index = request.Index,
            DisableIntroBump = request.DisableIntroBump,
            DisableInterStitials = request.DisableInterStitials
        };

        scheduleItemService.AddCollectionScheduleItem(newItem);

        return TypedResults.NoContent();
    }
}
