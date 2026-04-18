using FluentValidation;
using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.QueryScheduleItems;

public class UpdateQueryScheduleItem
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("/{id:int}", Handle)
            .WithRequestValidation<QueryScheduleItemRequest>()
            .WithName("UpdateQueryScheduleItem")
            .ProducesValidationProblem();
    }

    public class Validator : AbstractValidator<QueryScheduleItemRequest>
    {
        public Validator()
        {
            RuleFor(x => x.QueryId).GreaterThan(0);
            RuleFor(x => x.ScheduleItemId).GreaterThan(0);
            RuleFor(x => x.PlayDuration).GreaterThanOrEqualTo(0);
            RuleFor(x => x.PlayCount).GreaterThanOrEqualTo(0);
            RuleFor(x => x.PadTo).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Index).GreaterThanOrEqualTo(0);
            RuleFor(x => x.QueryType).IsInEnum();
            RuleFor(x => x.PlayoutType).IsInEnum();
        }
    }

    public record QueryScheduleItemRequest(int QueryId, int ScheduleItemId, int PlayDuration, int PlayCount, int PadTo, QueryType QueryType, PlayoutType PlayoutType, int Index);

    public static Results<Ok<int>, NotFound<string>, ValidationProblem> Handle(QueryScheduleItemService queryScheduleItemService, [FromRoute] int id, [FromBody] QueryScheduleItemRequest request)
    {
        var item = queryScheduleItemService.GetQueryScheduleItemByID(id);
        if (item is not null)
        {
            item.QueryId = request.QueryId;
            item.ScheduleItemId = request.ScheduleItemId;
            item.PlayDuration = request.PlayDuration;
            item.PlayCount = request.PlayCount;
            item.PadTo = request.PadTo;
            item.QueryType = request.QueryType;
            item.PlayoutType = request.PlayoutType;
            item.Index = request.Index;
            
            queryScheduleItemService.Update(item);
            return TypedResults.Ok(item.QueryScheduleItemId);
        }

        return TypedResults.NotFound($"A query schedule item with the ID: {id} was not found.");
    }
}
