using FluentValidation;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.ScheduleItems;

public class CreateScheduleItem
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", Handle)
            .WithRequestValidation<Validator>()
            .WithName("CreateScheduleItem")
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

    public static Results<Ok, NoContent, ValidationProblem> Handle(ScheduleItemService scheduleItemService, [FromBody] ScheduleItemRequest request)
    {
        var newScheduleItem = new Entities.ScheduleItem
        {
            Index = request.Index,
            Type = request.Type,
            ScheduleId = request.ScheduleId
        };

        scheduleItemService.AddScheduleItem(newScheduleItem);

        return TypedResults.NoContent();
    }
}
