using FluentValidation;
using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.CollectionScheduleItems;

public class UpdateCollectionScheduleItem
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("/{id:int}", Handle)
            .WithRequestValidation<CollectionScheduleItemRequest>()
            .WithName("UpdateCollectionScheduleItem")
            .ProducesValidationProblem();
    }

    public class Validator : AbstractValidator<CollectionScheduleItemRequest>
    {
        public Validator()
        {
            RuleFor(x => x.CollectionId).GreaterThan(0);
            RuleFor(x => x.ScheduleItemId).GreaterThan(0);
            RuleFor(x => x.PlayDuration).GreaterThanOrEqualTo(0);
            RuleFor(x => x.PlayCount).GreaterThanOrEqualTo(0);
            RuleFor(x => x.PadTo).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Index).GreaterThanOrEqualTo(0);
            RuleFor(x => x.CollectionType).IsInEnum();
            RuleFor(x => x.PlayoutType).IsInEnum();
        }
    }

    public record CollectionScheduleItemRequest(int CollectionId, int ScheduleItemId, int PlayDuration, int PlayCount, int PadTo, CollectionType CollectionType, PlayoutType PlayoutType, int Index);

    public static Results<Ok<int>, NotFound<string>, ValidationProblem> Handle(CollectionSchedulteItemService scheduleItemService, [FromRoute] int id, [FromBody] CollectionScheduleItemRequest request)
    {
        var item = scheduleItemService.GetCollectionScheduleItemByID(id);

        if (item is not null)
        {
            item.CollectionId = request.CollectionId;
            item.ScheduleItemId = request.ScheduleItemId;
            item.PlayDuration = request.PlayDuration;
            item.PlayCount = request.PlayCount;
            item.PadTo = request.PadTo;
            item.CollectionType = request.CollectionType;
            item.PlayoutType = request.PlayoutType;
            item.Index = request.Index;
            
            scheduleItemService.Update(item);
            return TypedResults.Ok(item.CollectionScheduleItemId);
        }

        return TypedResults.NotFound($"A collection schedule item with the ID: {id} was not found.");
    }
}
