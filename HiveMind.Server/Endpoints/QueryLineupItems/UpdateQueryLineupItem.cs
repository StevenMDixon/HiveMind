using FluentValidation;
using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.QueryLineupItems;

public class UpdateQueryLineupItem
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("/{id:int}", Handle)
            .WithRequestValidation<QueryLineupItemRequest>()
            .WithName("UpdateQueryLineupItem")
            .ProducesValidationProblem();
    }

    public class Validator : AbstractValidator<QueryLineupItemRequest>
    {
        public Validator()
        {
            RuleFor(x => x.QueryId).GreaterThan(0);
            RuleFor(x => x.LineupItemId).GreaterThan(0);
            RuleFor(x => x.PlayDuration).GreaterThanOrEqualTo(0);
            RuleFor(x => x.PlayCount).GreaterThanOrEqualTo(0);
            RuleFor(x => x.PadTo).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Index).GreaterThanOrEqualTo(0);
            RuleFor(x => x.QueryType).IsInEnum();
            RuleFor(x => x.PlayoutType).IsInEnum();
        }
    }

    public record QueryLineupItemRequest(int QueryId, int LineupItemId, int PlayDuration, int PlayCount, int PadTo, QueryType QueryType, PlayoutType PlayoutType, int Index);

    public static Results<Ok<int>, NotFound<string>, ValidationProblem> Handle(QueryLineupItemService queryLineupItemService, [FromRoute] int id, [FromBody] QueryLineupItemRequest request)
    {
        var item = queryLineupItemService.GetQueryLineupItemByID(id);
        if (item is not null)
        {
            item.QueryId = request.QueryId;
            item.LineupItemId = request.LineupItemId;
            item.PlayDuration = request.PlayDuration;
            item.PlayCount = request.PlayCount;
            item.PadTo = request.PadTo;
            item.QueryType = request.QueryType;
            item.PlayoutType = request.PlayoutType;
            item.Index = request.Index;
            
            queryLineupItemService.Update(item);
            return TypedResults.Ok(item.QueryLineupItemId);
        }

        return TypedResults.NotFound($"A query lineup item with the ID: {id} was not found.");
    }
}
