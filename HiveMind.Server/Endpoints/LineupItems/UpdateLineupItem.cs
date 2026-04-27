using FluentValidation;
using HiveMind.Server.Services;
using HiveMind.Server.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.LineupItems;

public class UpdateLineupItem
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("/{id:int}", Handle)
            .WithRequestValidation<LineupItemRequest>()
            .WithName("UpdateLineupItem")
            .ProducesValidationProblem();
    }

    public class Validator : AbstractValidator<LineupItemRequest>
    {
        public Validator()
        {
            RuleFor(x => x.Index).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Type).NotEmpty().MaximumLength(50);
            RuleFor(x => x.LineupId).GreaterThan(0);
        }
    }

    public record LineupItemRequest(int LineupItemId, string Name, int Index, string Type, int LineupId, ICollection<QueryLineupItem> Queries);

    public static Results<Ok, NotFound<string>, ValidationProblem> Handle(LineupItemService lineupItemService, [FromRoute] int id, [FromBody] LineupItemRequest request)
    {
        var lineupItem = lineupItemService.GetLineupItemByID(id);
        if (lineupItem is not null)
        {
            lineupItem.Name = request.Name;
            lineupItem.Index = request.Index;
            lineupItem.Type = request.Type;
            lineupItem.LineupId = request.LineupId;

            lineupItem.Queries = request.Queries;

            foreach (var item in lineupItem.Queries)
            {
                if (item.QueryLineupItemId < 0) item.QueryLineupItemId = 0;
            }

            lineupItemService.Update(lineupItem);
            return TypedResults.Ok();
        }

        return TypedResults.NotFound($"A lineup item with the ID: {id} was not found.");
    }
}
