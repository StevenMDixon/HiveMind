using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.ProgramStrategies;

public class Get
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:int}", Handle).WithName("GetProgramStrategyById");
    }

    public record ProgramStrategy(int Id, string Name, int AdvancedDays, bool Active, DateOnly? LastScheduleDate, List<StrategyLineup> ProgramStrategyItems, List<StrategyEvent> ProgramStrategyEvents);

    public record StrategyLineup(int Id, int ProgramStrategyId, int? LineUpId, LineupSelectionType SelectionType, String SelectionOption);

    public record StrategyEvent(int Id, string Name, int ProgramStrategyId, int PromoDays, int QueryId, DateOnly EventDate);

    public static Results<Ok<ProgramStrategy>, NotFound<string>> Handle(ProgramStrategyService programStrategyService, [FromRoute] int id)
    {
        var strategy = programStrategyService.GetProgramStrategyById(id);

        if (strategy is not null)
        {
            return TypedResults.Ok(new ProgramStrategy(
                strategy.Id,
                strategy.Name,
                strategy.AdvancedDays,
                strategy.Active,
                strategy.LastScheduleDate,
                strategy.Lineups?.Select(x => new StrategyLineup(x.Id, x.ProgramStrategyId!.Value, x.LineupId, x.SelectionType, x.SelectionOption)).ToList() ?? [],
                strategy.Events?.Select(x => new StrategyEvent(x.Id, x.Name, x.ProgramStrategyId!.Value, x.PromoDays, x.QueryId, x.EventDate)).ToList() ?? []
                ));
        }

        return TypedResults.NotFound($"A strategy with the ID: {id} was not found.");
    }
}
