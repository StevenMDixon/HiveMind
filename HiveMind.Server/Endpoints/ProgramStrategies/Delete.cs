using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.ProgramStrategies;

public class Delete
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id:int}", Handle)
            .WithName("DeleteProgramStrategy");
    }

    public static Results<Ok, NotFound<string>> Handle(ProgramStrategyService programStrategyService, [FromRoute] int id)
    {
        var strategy = programStrategyService.GetProgramStrategyById(id);

        if (strategy is not null)
        {
            programStrategyService.Delete(id);
            return TypedResults.Ok();
        }

        return TypedResults.NotFound($"A strategy with the ID: {id} was not found.");
    }
}
