using System.Reflection.Metadata;
using HiveMind.Server.Domain.Enums;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HiveMind.Server.Endpoints.Queries
{
    public class GetQueryTypes
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/types", Handle).WithName("GetQueryTypes");
        }

        public record Response(IEnumerable<string> Types);

        public static Results<Ok<Response>, NotFound<string>> Handle()
        {
            return TypedResults.Ok(new Response(Enum.GetValues<QueryType>().Select(x => x.ToString())));
        }
    }
}
