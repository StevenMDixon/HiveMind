using HiveMind.Server.Domain.Enums;
using Microsoft.AspNetCore.Http.HttpResults;
using static HiveMind.Server.Domain.Enums.QueryEnums;

namespace HiveMind.Server.Endpoints
{
    public class GetPadTo
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            var endpoints = app.MapGroup("/padto");
            endpoints.MapGet("/", Handle).WithName("Get Pad To Options");
        }

        public record PadToItem(int id, string name);

        public record PadToItemEnumResults(ICollection<PadToItem> options);

        public static Results<Ok<PadToItemEnumResults>, NotFound> Handle()
        {
            var options = Enum.GetValues<PadTo>();  

            return TypedResults.Ok(
                new PadToItemEnumResults(
                    options.Select(x => new PadToItem((int)x, x.ToString())).ToList()
                ));
        }
    }
}
