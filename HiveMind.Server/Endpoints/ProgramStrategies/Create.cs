using FluentValidation;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.ProgramStrategies;

public class Create
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", Handle)
            .WithRequestValidation<ProgramStrategyRequest>()
            .WithName("CreateProgramStrategy")
            .ProducesValidationProblem();
    }

    public class Validator : AbstractValidator<ProgramStrategyRequest>
    {
        public Validator()
        {
            RuleFor(x => x.Name).MaximumLength(100);
            RuleFor(x => x.AdvancedDays).NotEmpty().GreaterThan(-1);
        }
    }

    public record ProgramStrategyRequest(string Name, int? AdvancedDays, bool? Active, DateOnly? StartDate, DateOnly? EndDate);

    public static Results<Ok, NoContent, ValidationProblem> Handle(ProgramStrategyService programStrategyService, [FromBody] ProgramStrategyRequest request)
    {
        var newProgramStrategy = new Entities.ProgramStrategy
        {
            Name =  request.Name,
            AdvancedDays =  request.AdvancedDays ?? 0,
            Active = request.Active ?? false,
            StartDate =  request.StartDate,
            EndDate = request.EndDate
        };

        programStrategyService.AddProgramStrategy(newProgramStrategy);
        return TypedResults.NoContent();
    }
}
