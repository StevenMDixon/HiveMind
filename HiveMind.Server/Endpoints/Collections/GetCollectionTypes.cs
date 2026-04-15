using System.Reflection.Metadata;
using HiveMind.Server.Domain.Enums;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HiveMind.Server.Endpoints.Collections
{
    public class GetCollectionTypes
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/types", Handle).WithName("GetCollectionTypes");
        }

        public record Response(IEnumerable<string> types);

        public static Results<Ok<Response>, NotFound<string>> Handle()
        {
            return TypedResults.Ok(new Response(Enum.GetValues<CollectionType>().Select(x => x.ToString())));
        }
    }
}
