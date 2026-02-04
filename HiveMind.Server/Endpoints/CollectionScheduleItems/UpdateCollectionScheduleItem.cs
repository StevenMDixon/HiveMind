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
            .WithRequestValidation<Validator>()
            .WithName("UpdateCollectionScheduleItem")
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

    public static Results<Ok, NotFound<string>, ValidationProblem> Handle(CollectionSchedulteItemService scheduleItemService, [FromRoute] int id, [FromBody] CollectionScheduleItemRequest request)
    {
        var item = scheduleItemService.GetCollectionScheduleItemByID(id);

        if (item is not null)
        {
            item.CollectionId = request.CollectionId;
            item.ScheduleItemId = request.ScheduleItemId;
            item.Duration = request.Duration;
            item.CollectionType = request.CollectionType;
            item.PlayoutType = request.PlayoutType;
            item.Index = request.Index;
            item.DisableIntroBump = request.DisableIntroBump;
            item.DisableInterStitials = request.DisableInterStitials;
            
            scheduleItemService.Update(item);
            return TypedResults.Ok();
        }

        return TypedResults.NotFound($"A collection schedule item with the ID: {id} was not found.");
    }
}
