using FluentValidation;
using HiveMind.Server.Entities;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Lineup;

public class Update
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
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        }
    }

    public record LineupRequest(string? Name, TimeOnly? StartTime, string? JsonData);
    public static Results<Ok, NotFound<string>, ValidationProblem> Handle(LineupService lineupService, [FromRoute] int id, [FromBody] LineupRequest request)
    {
        var lineup = lineupService.GetLineupByID(id);

        if (lineup is not null)
        {
            lineup.Name = request?.Name ?? lineup.Name;
            lineup.StartTime = request?.StartTime ?? lineup.StartTime;
            lineup.JsonData = request?.JsonData ?? lineup.JsonData;

            lineupService.Update(lineup);
            return TypedResults.Ok();
        }

        return TypedResults.NotFound($"A lineup with the ID: {id} was not found.");
    }
}
