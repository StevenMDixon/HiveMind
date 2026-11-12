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

    public record ProgramStrategyRequest(string? Name, int? AdvancedDays, bool? Active, DateOnly? StartDate, DateOnly? EndDate, ICollection<ProgramStrategyItem> ProgramStrategyItems, ICollection<ProgramStrategyEvent> ProgramStrategyEvents);

    public record ProgramStrategyItem(int Id, int? ProgramStrategyId, int? LineUpId, LineupSelectionType SelectionType, String SelectionOption);

    public record ProgramStrategyEvent(int Id, int? ProgramStrategyId, string Name, int PromoDays, int QueryId, DateOnly EventDate);

    public static Results<Ok, NotFound<string>, ValidationProblem> Handle(ProgramStrategyService programStrategyService, [FromRoute] int id, [FromBody] ProgramStrategyRequest request)
    {
        var strategy = programStrategyService.GetProgramStrategyById(id);

        if (strategy != null)
        {
            strategy.Name = request.Name ?? strategy.Name;
            strategy.AdvancedDays = request.AdvancedDays ?? strategy.AdvancedDays;
            strategy.Active = request.Active ?? strategy.Active;

            strategy.Lineups = request.ProgramStrategyItems.Select(x => ConvertAndUpdateLineup(strategy.Id, x, strategy.Lineups)).ToList();
            strategy.Events = request.ProgramStrategyEvents.Select(x => ConvertAndUpdateEvent(strategy.Id, x, strategy.Events)).ToList();

            programStrategyService.Update(strategy);
            return TypedResults.Ok();
        }

        return TypedResults.NotFound($"A strategy with the ID: {id} was not found.");
    }

   private static ProgramStrategyLineup ConvertAndUpdateLineup(int parentID, ProgramStrategyItem updatedItem, ICollection<ProgramStrategyLineup>? currentItems)
    {
        var matched = currentItems?.Where(x => x.Id == updatedItem.Id).FirstOrDefault() ?? null;

        if (matched != null)
        {
            matched.SelectionOption = updatedItem.SelectionOption;
            matched.SelectionType = updatedItem.SelectionType;
            matched.LineupId = updatedItem.LineUpId;

            return matched;
        }

        return new ProgramStrategyLineup() { LineupId = updatedItem.LineUpId, SelectionOption = updatedItem.SelectionOption, ProgramStrategyId = parentID, SelectionType = updatedItem.SelectionType };
    }

    private static ProgramEvent ConvertAndUpdateEvent(int parentID, ProgramStrategyEvent updatedItem, ICollection<ProgramEvent>? currentItems)
    {
        var matched = currentItems?.Where(x => x.Id == updatedItem.Id).FirstOrDefault() ?? null;

        if (matched != null)
        {
            matched.QueryId = updatedItem.QueryId;
            matched.Name = updatedItem.Name;
            matched.EventDate = updatedItem.EventDate;
            matched.PromoDays = updatedItem.PromoDays;

            return matched;
        }

        return new ProgramEvent() { ProgramStrategyId = parentID, QueryId = updatedItem.QueryId, Name = updatedItem.Name, EventDate = updatedItem.EventDate, PromoDays = updatedItem.PromoDays };
    }
}
