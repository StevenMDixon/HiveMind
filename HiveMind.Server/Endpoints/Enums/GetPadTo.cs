using HiveMind.Server.Domain.Enums;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HiveMind.Server.Endpoints.Enums;

public class GetPadTo
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("/padto");
        endpoints.MapGet("/", Handle).WithName("Get Pad To Options");
    }

    public record Response(Dictionary<int, string> Options);

    public static Results<Ok<Response>, NotFound> Handle()
    {
        var options = Enum.GetValues<PadTo>()
           .ToDictionary(t => (int)t, t => t.ToString());

        return TypedResults.Ok(new Response(options));
    }
}
