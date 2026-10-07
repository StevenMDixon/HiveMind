using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;


namespace HiveMind.Server.Endpoints.Shows;

public class Get
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:int}", Handle).WithName("GetAllShows");
    }

    public record Show(int Id, string Name, string Rating);

    public record ShowsResponse(ICollection<Show> Shows);

    public static Results<Ok<ShowsResponse>, NotFound<string>> Handle(ShowService showService, [FromRoute] int id)
    {
        var show = showService.GetShowByID(id);
        
        if (show is not null)
        {
            return TypedResults.Ok(new ShowsResponse(new List<Show> { new Show(show.Id, show.Name, show.Rating.ToString()) }));
        }

        return TypedResults.NotFound($"A show with the ID: {id} was not found.");
    }
}