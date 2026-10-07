using FluentValidation;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Lineup;

public class Create
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
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        }
    }

    public record LineupRequest(string Name, TimeOnly StartTime);

    public static Results<Ok, NoContent, ValidationProblem> Handle(LineupService lineupService, [FromBody] LineupRequest request)
    {
        var newLineup = new Entities.Lineup
        {
            Name = request.Name,
            StartTime = request.StartTime
        };

        lineupService.AddLineup(newLineup);
        return TypedResults.NoContent();
    }
}
