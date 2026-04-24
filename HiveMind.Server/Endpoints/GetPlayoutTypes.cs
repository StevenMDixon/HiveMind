using HiveMind.Server.Domain.Enums;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HiveMind.Server.Endpoints;

public class GetPlayoutTypes
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("/playout-types");
        endpoints.MapGet("/", Handle).WithName("Get Playout Types");
    }

    public record PlayOutItem(int id, string name);

    public record Response(ICollection<PlayOutItem> types);

    public static Results<Ok<Response>, NotFound<string>> Handle()
    {
        return TypedResults.Ok(new Response(
            Enum.GetValues<PlayoutType>().Select(x => new PlayOutItem((int)x, x.ToString())).ToList()));
    }
}
