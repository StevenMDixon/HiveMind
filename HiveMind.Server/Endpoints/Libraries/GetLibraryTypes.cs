using HiveMind.Server.Domain.Enums;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HiveMind.Server.Endpoints.Libraries;

public class GetLibraryTypes
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/types", Handle).WithName("GetLibraryTypes");
    }

    public record Response(Dictionary<int, string> types);

    public static Results<Ok<Response>, NotFound<string>> Handle()
    {
        return TypedResults.Ok(
            new Response(Enum.GetValues<LibraryType>()
               .ToDictionary(t => (int)t, t => t.ToString())));
    }
}
