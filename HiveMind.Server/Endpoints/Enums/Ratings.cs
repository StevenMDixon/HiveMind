using HiveMind.Server.Domain.Enums;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HiveMind.Server.Endpoints.Enums;

public class Ratings
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("/ratings");
        endpoints.MapGet("/", Handle).WithName("Get Ratings");
    }

    public record Response(Dictionary<int, string> Types);

    public static Results<Ok<Response>, NotFound<string>> Handle()
    {
        return TypedResults.Ok(new Response(
            Enum.GetValues<Domain.Enums.Ratings>()
               .ToDictionary(t => (int)t, t => t.ToString())));
    }
}
