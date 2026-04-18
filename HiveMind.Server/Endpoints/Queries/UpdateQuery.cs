using FluentValidation;
using HiveMind.Server.Entities;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Queries;

public class UpdateQuery
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("/{id:int}", Handle)
            .WithRequestValidation<QueryRequest>()
            .WithName("UpdateQuery")
            .ProducesValidationProblem();
    }

    public class Validator : AbstractValidator<QueryRequest>
    {
        public Validator()
        {
            RuleFor(x => x.QueryName).NotEmpty();
        }
    }


    public record QueryRequest(string QueryName, ICollection<QueryFilters> Filters);

    public static Results<Ok, NotFound<string>, ValidationProblem> Handle(QueryService queryService, [FromRoute] int id, [FromBody] QueryRequest request)
    {
        var query = queryService.GetQueryByID(id);

        if (query is not null)
        {
            query.Name = request.QueryName;
            query.Filters = request.Filters;
            foreach (var item in query.Filters)
            {
                if (item.QueryFilterId < 0) item.QueryFilterId = 0;
            }
            queryService.Update(query);
            return TypedResults.Ok();
        }

        return TypedResults.NotFound($"A query with the ID: {id} was not found.");
    }
}
