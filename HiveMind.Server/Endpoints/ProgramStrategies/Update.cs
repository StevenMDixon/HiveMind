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

    public record ProgramStrategyItem(int Id, int? ProgramStrategyId, int? LineUpId, LineupSelectionType SelectionType, String SelectionOption);

    public static Results<Ok, NotFound<string>, ValidationProblem> Handle(ProgramStrategyService programStrategyService, [FromRoute] int id, [FromBody] ProgramStrategyRequest request)
    {
        var strategy = programStrategyService.GetProgramStrategyById(id);

        if (strategy is not null)
        {
            strategy.Name = request.Name ?? strategy.Name;
            strategy.AdvancedDays = request.AdvancedDays ?? strategy.AdvancedDays;
            strategy.Active = request.Active ?? strategy.Active;

            var itemsToUpdate = new List<(ProgramStrategyLineup, ProgramStrategyItem)>();

            foreach(var updatedLineup in request.ProgramStrategyItems)
            {
                var matchedItem = strategy?.Lineups?.Where(x => x.Id == updatedLineup.Id).FirstOrDefault();

                if(matchedItem != null)
                {
                    itemsToUpdate.Add((matchedItem, updatedLineup));
                }

                itemsToUpdate.Add((new ProgramStrategyLineup() { 
                    ProgramStrategyId = strategy!.Id, SelectionOption = updatedLineup.SelectionOption, SelectionType = updatedLineup.SelectionType}, updatedLineup));
            }

            strategy.Lineups = Helper.Resolve(itemsToUpdate, UpdateItem);

            programStrategyService.Update(strategy);
            return TypedResults.Ok();
        }

        return TypedResults.NotFound($"A strategy with the ID: {id} was not found.");
    }

    private static ProgramStrategyLineup UpdateItem(ProgramStrategyLineup lineup, ProgramStrategyItem updatedLineup)
    {
        if(updatedLineup != null)
        {
            lineup.Id = updatedLineup.Id;
            lineup.SelectionOption = updatedLineup.SelectionOption;
            lineup.SelectionType = updatedLineup.SelectionType;
            lineup.LineupId = updatedLineup.LineUpId;
        }

        return lineup;
    }
}
