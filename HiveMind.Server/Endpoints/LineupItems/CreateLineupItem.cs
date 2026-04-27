using FluentValidation;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.LineupItems;

public class CreateLineupItem
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", Handle)
            .WithRequestValidation<LineupItemRequest>()
            .WithName("CreateLineupItem")
            .ProducesValidationProblem();
    }

    public class Validator : AbstractValidator<LineupItemRequest>
    {
        public Validator()
        {
            RuleFor(x => x.Index).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Type).NotEmpty().MaximumLength(50);
            RuleFor(x => x.LineupId).GreaterThan(0);
        }
    }

    public record LineupItemRequest(int Index, string Type, int LineupId, string Name);
    public static Results<Ok<int>, NoContent, ValidationProblem> Handle(LineupItemService lineupItemService, [FromBody] LineupItemRequest request)
    {
        var newLineupItem = new Entities.LineupItem
        {
            Name = request.Name,
            Index = request.Index,
            Type = request.Type,
            LineupId = request.LineupId
        };

        var result = lineupItemService.AddLineupItem(newLineupItem);

        return TypedResults.Ok(result.LineupItemId);
    }
}
