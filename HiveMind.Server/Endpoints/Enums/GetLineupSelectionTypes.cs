using HiveMind.Server.Domain.Enums;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HiveMind.Server.Endpoints.Enums;

public class GetLineupSelectionTypes
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("/LineupSelection");
        endpoints.MapGet("/", Handle).WithName("Get Lineup Selection Options");
    }

    public record Response(Dictionary<int, string> Options);

    public static Results<Ok<Response>, NotFound> Handle()
    {
        var options = Enum.GetValues<LineupSelectionType>()
           .ToDictionary(t => (int)t, t => t.ToString());

        return TypedResults.Ok(new Response(options));
    }
}
