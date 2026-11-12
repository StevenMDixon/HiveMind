using HiveMind.Server.Domain.Enums;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HiveMind.Server.Endpoints.Tags;

public class GetTagCategories
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/categories", Handle).WithName("GetTagCategories");
    }

    public record Response(Dictionary<int, string> categories);

    public static Results<Ok<Response>, NotFound<string>> Handle()
    {
        return TypedResults.Ok(
            new Response(Enum.GetValues<TagCategory>()
               .ToDictionary(t => (int)t, t => t.ToString())));
    }
}
