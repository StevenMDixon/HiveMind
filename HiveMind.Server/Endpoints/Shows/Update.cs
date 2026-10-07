using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Shows;

public class Update
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/{id:int}", Handle)
            .WithName("UpdateShow");
    }

    public record ShowRequest(string? Name, Ratings? Rating);

    public static Results<Ok, NotFound<string>, ValidationProblem> Handle(ShowService showService, [FromRoute] int id, [FromBody] ShowRequest request)
    {
        var show = showService.GetShowByID(id);

        if (show is not null)
        {
            show.Name = request.Name ?? show.Name;
            show.Rating = request.Rating ?? show.Rating;
            showService.Update(show);
            return TypedResults.Ok();
        }

        return TypedResults.NotFound($"A show with the ID: {id} was not found.");
    }
}
