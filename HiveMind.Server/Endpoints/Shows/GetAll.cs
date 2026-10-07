using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;


namespace HiveMind.Server.Endpoints.Shows;

public class GetAll
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", Handle).WithName("GetAllShows");
    }

    public record Show(int Id, string Name, string Rating);

    public record ShowsResponse(ICollection<Show> Shows);

    public static Results<Ok<ShowsResponse>, NotFound> Handle(ShowService showService)
    {
        var shows = showService.GetAllShows();
        
        return TypedResults.Ok(new ShowsResponse(shows.Select(show => new Show(show.Id, show.Name, show.Rating.ToString())).ToList()));
    }
}