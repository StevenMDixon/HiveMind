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

    public record ProgramStrategy(int ProgramStrategyId, string Name, int AdvancedDays, bool Active, DateOnly? LastScheduleDate, List<StrategyLineup> ProgramStrategyItems);

    public record StrategyLineup(int ProgramStrategyLineupId, int? ProgramStrategyId, int? LineUpId, LineupSelectionType SelectionType, String SelectionOption);

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
                strategy.LastScheduleDate,
                strategy.Lineups?.Select(x => new StrategyLineup(x.ProgramStrategyLineupId, x.ProgramStrategyId, x.LineupId, x.SelectionType, x.SelectionOption)).ToList() ?? []
                ));
        }

        return TypedResults.NotFound($"A strategy with the ID: {id} was not found.");
    }
}
