using FluentValidation;
using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Entities;
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

    public record ProgramStrategyRequest(string? Name, int? AdvancedDays, bool? Active, DateOnly? StartDate, DateOnly? EndDate, ICollection<ProgramStrategyItem> ProgramStrategyItems);

    public record ProgramStrategyItem(int ProgramStrategyLineupId, int? ProgramStrategyId, int? LineUpId, LineupSelectionType SelectionType, String SelectionOption);

    public static Results<Ok, NotFound<string>, ValidationProblem> Handle(ProgramStrategyService programStrategyService, [FromRoute] int id, [FromBody] ProgramStrategyRequest request)
    {
        var strategy = programStrategyService.GetProgramStrategyById(id);

        if (strategy is not null)
        {
            strategy.Name = request.Name ?? strategy.Name;
            strategy.AdvancedDays = request.AdvancedDays ?? strategy.AdvancedDays;
            strategy.Active = request.Active ?? strategy.Active;


            var strategyLineupToRemove = strategy.Lineups?.Where(c => !request.ProgramStrategyItems.Any(x => x.ProgramStrategyLineupId == c.ProgramStrategyLineupId)).ToList();

            foreach (var stratToRem in strategyLineupToRemove ?? [])
            {
                strategy.Lineups.Remove(stratToRem);
            }

            foreach(var stratToUpd in strategy.Lineups ?? [])
            {
                var reqData = request.ProgramStrategyItems.Where(x => x.ProgramStrategyLineupId == stratToUpd.ProgramStrategyLineupId).FirstOrDefault();

                if(reqData != null)
                {
                    stratToUpd.SelectionOption = reqData.SelectionOption;
                    stratToUpd.SelectionType = reqData.SelectionType;
                    stratToUpd.LineupId = reqData.LineUpId;
                }
            }

            var strategyLineupsToAdd = request.ProgramStrategyItems.Where(x => x.ProgramStrategyLineupId <= 0).ToList();

            foreach(var stratToAdd in strategyLineupsToAdd)
            {
                strategy.Lineups?.Add(new ProgramStrategyLineup() { LineupId = stratToAdd.LineUpId == 0 ? null : stratToAdd.LineUpId, ProgramStrategyId = strategy.ProgramStrategyId, SelectionOption = stratToAdd.SelectionOption, SelectionType = stratToAdd.SelectionType });
            }

            programStrategyService.Update(strategy);
            return TypedResults.Ok();
        }

        return TypedResults.NotFound($"A strategy with the ID: {id} was not found.");
    }
}
