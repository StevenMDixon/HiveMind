using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;


namespace HiveMind.Server.Endpoints.Shows;

public class GetShows
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", Handle).WithName("GetAllShows");
    }

    public record Show(int ShowId, string ShowName);

    public record ShowsResponse(ICollection<Show> Shows);

    public static Results<Ok<ShowsResponse>, NotFound> Handle(ShowService showService)
    {
        var shows = showService.GetAllShows();
        
        return TypedResults.Ok(new ShowsResponse(shows.Select(show => new Show(show.ShowId, show.ShowTitle)).ToList()));
    }

}