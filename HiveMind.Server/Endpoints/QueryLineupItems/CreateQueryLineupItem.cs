using FluentValidation;
using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.QueryLineupItems;

public class CreateQueryLineupItem
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", Handle)
            .WithRequestValidation<QueryLineupItemRequest>()
            .WithName("CreateQueryLineupItem")
            .ProducesValidationProblem();
    }

    public class Validator : AbstractValidator<QueryLineupItemRequest>
    {
        public Validator()
        {
            RuleFor(x => x.LineupItemId).GreaterThan(0);
            RuleFor(x => x.Duration).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Count).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Index).GreaterThanOrEqualTo(0);
            RuleFor(x => x.QueryType).IsInEnum();
            RuleFor(x => x.PlayoutType).IsInEnum();
        }
    }

    public record QueryLineupItemRequest(int QueryId, int LineupItemId, int Duration, int Count, QueryType QueryType, PlayoutType PlayoutType, int Index);

    public static Results<Ok<int>, NoContent, ValidationProblem> Handle(QueryLineupItemService queryLineupItemService, [FromBody] QueryLineupItemRequest request)
    {
        var newItem = new Entities.QueryLineupItem
        {
            QueryId = request.QueryId,
            LineupItemId = request.LineupItemId,
            PlayDuration = request.Duration,
            PlayCount = request.Count,
            QueryType = request.QueryType,
            PlayoutType = request.PlayoutType,
            Index = request.Index
        };

        queryLineupItemService.AddQueryLineupItem(newItem);

        return TypedResults.Ok(newItem.QueryLineupItemId);
    }
}
