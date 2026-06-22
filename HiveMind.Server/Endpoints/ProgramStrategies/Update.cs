using FluentValidation;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.ProgramStrategies;

public class Update
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("/{id:int}", Handle)
            .WithRequestValidation<ProgramStrategyRequest>()
            .WithName("UpdateStrategy")
            .ProducesValidationProblem();
    }

    public class Validator : AbstractValidator<ProgramStrategyRequest>
    {
        public Validator()
        {
            RuleFor(x => x.AdvancedDays).GreaterThanOrEqualTo(0).When(x => x.AdvancedDays != null);
            RuleFor(x => x.Name).NotEmpty().When(x => x.Name != null);
        }
    }

    public record ProgramStrategyRequest(string? Name, int? AdvancedDays, bool? Active, DateOnly? StartDate, DateOnly? EndDate);
    public static Results<Ok, NotFound<string>, ValidationProblem> Handle(ProgramStrategyService programStrategyService, [FromRoute] int id, [FromBody] ProgramStrategyRequest request)
    {
        var strategy = programStrategyService.GetProgramStrategyById(id);

        if (strategy is not null)
        {
            strategy.Name = request.Name ?? strategy.Name;
            strategy.AdvancedDays = request.AdvancedDays ?? strategy.AdvancedDays;
            strategy.Active = request.Active ?? strategy.Active;
            strategy.StartDate = request.StartDate ?? request.StartDate;
            strategy.EndDate = request.StartDate ?? request.EndDate;

            programStrategyService.Update(strategy);
            return TypedResults.Ok();
        }

        return TypedResults.NotFound($"A strategy with the ID: {id} was not found.");
    }
}
