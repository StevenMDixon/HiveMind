using FluentValidation;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Queries;

public class CreateQuery
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", Handle)
            .WithRequestValidation<QueryRequest>()
            .WithName("CreateQuery")
            .ProducesValidationProblem();
    }

    public class Validator : AbstractValidator<QueryRequest>
    {
        public Validator()
        {
            RuleFor(x => x.QueryName).NotEmpty();
        }
    }

    public record Response(int QueryId);

    public record QueryRequest(string QueryName);
    public static Results<Ok<Response>, NoContent, ValidationProblem> Handle(QueryService queryService, [FromBody] QueryRequest request)
    {
        var newQuery = new Entities.Query
        {
            Name = request.QueryName
        };

        newQuery = queryService.AddQuery(newQuery);

        return TypedResults.Ok(new Response(newQuery.QueryId));
    }
}
