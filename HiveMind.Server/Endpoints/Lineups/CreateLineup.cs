using FluentValidation;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Lineup;

public class CreateLineup
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", Handle)
            .WithRequestValidation<LineupRequest>()
            .WithName("CreateLineup")
            .ProducesValidationProblem();
    }

    public class Validator : AbstractValidator<LineupRequest>
    {
        public Validator()
        {
            RuleFor(x => x.LineupName).NotEmpty().MaximumLength(100);
        }
    }

    public record LineupRequest(string LineupName, TimeOnly StartTime);

    public static Results<Ok, NoContent, ValidationProblem> Handle(LineupService lineupService, [FromBody] LineupRequest request)
    {
        var newLineup = new Entities.Lineup
        {
            LineupName = request.LineupName,
            StartTime = request.StartTime
        };

        lineupService.AddLineup(newLineup);
        return TypedResults.NoContent();
    }
}
