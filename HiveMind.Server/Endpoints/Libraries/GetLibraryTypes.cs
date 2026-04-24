using HiveMind.Server.Domain.Enums;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HiveMind.Server.Endpoints.Libraries;

public class GetLibraryTypes
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/types", Handle).WithName("GetLibraryTypes");
    }

    public record LibraryTypeItem(int id, string name);

    public record Response(ICollection<LibraryTypeItem> types);

    public static Results<Ok<Response>, NotFound<string>> Handle()
    {
        return TypedResults.Ok(
            new Response(Enum.GetValues<LibraryType>().Select(x => new LibraryTypeItem((int)x, x.ToString())).ToList()));
    }
}
