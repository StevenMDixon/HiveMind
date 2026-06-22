using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.ProgramStrategies;

public class GetById
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:int}", Handle).WithName("GetProgramStrategyById");
    }

    public record ProgramStrategy(int ProgramStrategyId, string Name, int AdvancedDays, bool Active, DateOnly? StartDate, DateOnly? EndDate);

    public static Results<Ok<ProgramStrategy>, NotFound<string>> Handle(ProgramStrategyService programStrategyService, [FromRoute] int id)
    {
        var strategy = programStrategyService.GetProgramStrategyById(id);

        if (strategy is not null)
        {
            return TypedResults.Ok(new ProgramStrategy(
                strategy.ProgramStrategyId,
                strategy.Name,
                strategy.AdvancedDays,
                strategy.Active,
                strategy.StartDate,
                strategy.EndDate
            ));
        }

        return TypedResults.NotFound($"A strategy with the ID: {id} was not found.");
    }
}
