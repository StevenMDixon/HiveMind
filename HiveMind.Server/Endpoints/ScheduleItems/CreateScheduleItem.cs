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
            .WithRequestValidation<ScheduleItemRequest>()
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

    public record ScheduleItemRequest(int Index, string Type, int ScheduleId, string Name);

    public static Results<Ok<int>, NoContent, ValidationProblem> Handle(ScheduleItemService scheduleItemService, [FromBody] ScheduleItemRequest request)
    {
        var newScheduleItem = new Entities.ScheduleItem
        {
            Name = request.Name,
            Index = request.Index,
            Type = request.Type,
            ScheduleId = request.ScheduleId
        };

        var result = scheduleItemService.AddScheduleItem(newScheduleItem);

        return TypedResults.Ok(result.ScheduleItemId);
    }
}
