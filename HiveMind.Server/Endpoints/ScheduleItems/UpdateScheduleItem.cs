using FluentValidation;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.ScheduleItems;

public class UpdateScheduleItem
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("/{id:int}", Handle)
            .WithRequestValidation<Validator>()
            .WithName("UpdateScheduleItem")
            .ProducesValidationProblem();
    }

    public class Validator : AbstractValidator<ScheduleItemRequest>
    {
        public Validator()
        {
            RuleFor(x => x.Index).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Type).NotEmpty().MaximumLength(50);
            RuleFor(x => x.ScheduleId).GreaterThan(0);
        }
    }

    public record ScheduleItemRequest(int Index, string Type, int ScheduleId);

    public static Results<Ok, NotFound<string>, ValidationProblem> Handle(ScheduleItemService scheduleItemService, [FromRoute] int id, [FromBody] ScheduleItemRequest request)
    {
        var scheduleItem = scheduleItemService.GetScheduleItemByID(id);

        if (scheduleItem is not null)
        {
            scheduleItem.Index = request.Index;
            scheduleItem.Type = request.Type;
            scheduleItem.ScheduleId = request.ScheduleId;
            scheduleItemService.Update(scheduleItem);
            return TypedResults.Ok();
        }

        return TypedResults.NotFound($"A schedule item with the ID: {id} was not found.");
    }
}
