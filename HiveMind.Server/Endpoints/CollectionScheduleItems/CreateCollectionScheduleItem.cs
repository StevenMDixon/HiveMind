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
            .WithRequestValidation<CollectionScheduleItemRequest>()
            .WithName("CreateCollectionScheduleItem")
            .ProducesValidationProblem();
    }

    public class Validator : AbstractValidator<CollectionScheduleItemRequest>
    {
        public Validator()
        {
            //RuleFor(x => x.CollectionId).GreaterThan(0);
            RuleFor(x => x.ScheduleItemId).GreaterThan(0);
            RuleFor(x => x.Duration).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Count).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Index).GreaterThanOrEqualTo(0);
            RuleFor(x => x.CollectionType).IsInEnum();
            RuleFor(x => x.PlayoutType).IsInEnum();
        }
    }

    public record CollectionScheduleItemRequest(int CollectionId, int ScheduleItemId, int Duration, int Count, CollectionType CollectionType, PlayoutType PlayoutType, int Index);

    public static Results<Ok<int>, NoContent, ValidationProblem> Handle(CollectionSchedulteItemService scheduleItemService, [FromBody] CollectionScheduleItemRequest request)
    {
        var newItem = new Entities.CollectionScheduleItem
        {
            CollectionId = request.CollectionId,
            ScheduleItemId = request.ScheduleItemId,
            PlayDuration = request.Duration,
            PlayCount = request.Count,
            CollectionType = request.CollectionType,
            PlayoutType = request.PlayoutType,
            Index = request.Index
        };

        scheduleItemService.AddCollectionScheduleItem(newItem);

        return TypedResults.Ok(newItem.CollectionScheduleItemId);
    }
}
