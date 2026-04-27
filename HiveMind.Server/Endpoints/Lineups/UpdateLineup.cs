using FluentValidation;
using HiveMind.Server.Entities;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Lineup;

public class UpdateLineup
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("/{id:int}", Handle)
            .WithRequestValidation<LineupRequest>()
            .WithName("UpdateLineup")
            .ProducesValidationProblem();
    }

    public class Validator : AbstractValidator<LineupRequest>
    {
        public Validator()
        {
            RuleFor(x => x.LineupName).NotEmpty().MaximumLength(100);
        }
    }

    public record LineupRequest(string? LineupName, TimeOnly? StartTime, List<LineupItem>? LineupItems);
    public static Results<Ok, NotFound<string>, ValidationProblem> Handle(LineupService lineupService, [FromRoute] int id, [FromBody] LineupRequest request)
    {
        var lineup = lineupService.GetLineupByID(id);

        if (lineup is not null)
        {
            lineup.LineupName = request?.LineupName ?? lineup.LineupName;
            lineup.StartTime = request?.StartTime ?? lineup.StartTime;
            lineup.LineupItems = request?.LineupItems ?? lineup.LineupItems;

            foreach (var item in lineup.LineupItems ?? [])
            {
                if (item.LineupItemId < 0) item.LineupItemId = 0;
            }

            lineupService.Update(lineup);
            return TypedResults.Ok();
        }

        return TypedResults.NotFound($"A lineup with the ID: {id} was not found.");
    }
}
