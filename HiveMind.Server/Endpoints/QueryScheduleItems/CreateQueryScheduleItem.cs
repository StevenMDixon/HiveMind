using FluentValidation;
using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.QueryScheduleItems;

public class CreateQueryScheduleItem
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", Handle)
            .WithRequestValidation<QueryScheduleItemRequest>()
            .WithName("CreateQueryScheduleItem")
            .ProducesValidationProblem();
    }

    public class Validator : AbstractValidator<QueryScheduleItemRequest>
    {
        public Validator()
        {
            RuleFor(x => x.ScheduleItemId).GreaterThan(0);
            RuleFor(x => x.Duration).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Count).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Index).GreaterThanOrEqualTo(0);
            RuleFor(x => x.QueryType).IsInEnum();
            RuleFor(x => x.PlayoutType).IsInEnum();
        }
    }

    public record QueryScheduleItemRequest(int QueryId, int ScheduleItemId, int Duration, int Count, QueryType QueryType, PlayoutType PlayoutType, int Index);

    public static Results<Ok<int>, NoContent, ValidationProblem> Handle(QueryScheduleItemService queryScheduleItemService, [FromBody] QueryScheduleItemRequest request)
    {
        var newItem = new Entities.QueryScheduleItem
        {
            QueryId = request.QueryId,
            ScheduleItemId = request.ScheduleItemId,
            PlayDuration = request.Duration,
            PlayCount = request.Count,
            QueryType = request.QueryType,
            PlayoutType = request.PlayoutType,
            Index = request.Index
        };

        queryScheduleItemService.AddQueryScheduleItem(newItem);

        return TypedResults.Ok(newItem.QueryScheduleItemId);
    }
}
