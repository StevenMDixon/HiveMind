using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HiveMind.Server.Endpoints.ProgramStrategies;

public class GetAll
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", Handle).WithName("GetAllProgramStrategies");
    }

    public record ProgramStrategy(int ProgramStrategyId, string Name, int AdvancedDays, bool Active, DateOnly? LastScheduledDate);
    public record GetAllProgramStrategiesResponse(List<ProgramStrategy> Strategies);
    public static Results<Ok<GetAllProgramStrategiesResponse>, NotFound> Handle(ProgramStrategyService programStrategy)
    {
        var strategies = programStrategy.GetAllProgramStrategies();

        return TypedResults.Ok(new GetAllProgramStrategiesResponse(strategies.Select(x => new ProgramStrategy(x.ProgramStrategyId, x.Name, x.AdvancedDays, x.Active, x.LastScheduleDate)).ToList()));
    }
}
