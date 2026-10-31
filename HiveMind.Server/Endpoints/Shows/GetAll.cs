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

    public record Parameters(
       string? Search,
       int? Page,
       int? Count
    );

    public static Results<Ok<ShowsResponse>, NotFound> Handle(ShowService showService, [AsParameters] Parameters filters)
    {
        var shows = showService.GetAllShows();

        var pageSize = filters.Count ?? 100;
        var currentPage = filters.Page ?? 1;
        var dataCount = shows.Count();

        if (filters.Search != null && filters.Search != string.Empty) shows = shows.Where(x => x.Name.ToLower().Contains(filters.Search.ToLower()));

        return TypedResults.Ok(new ShowsResponse(shows.Select(show => new Show(show.Id, show.Name, show.Rating.ToString())).ToList()));
    }
}