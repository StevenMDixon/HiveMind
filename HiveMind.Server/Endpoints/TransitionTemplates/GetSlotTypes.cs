using Microsoft.AspNetCore.Http.HttpResults;

namespace HiveMind.Server.Endpoints.TransitionTemplates;

public class GetSlotTypes
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/slot-types", Handle)
            .WithName("GetAllSlotTypes");
    }

    public record Response(Dictionary<int, string> Slots);

    public static Results<Ok<Response>, NotFound<string>> Handle()
    {
        return TypedResults.Ok(new Response(
            Enum.GetValues<Domain.Enums.TransitionSlot>()
               .ToDictionary(t => (int)t, t => t.ToString())));
    }
}
