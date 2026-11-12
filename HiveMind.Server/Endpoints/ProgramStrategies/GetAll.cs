using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using static HiveMind.Server.Endpoints.MediaItems.GetAll;

namespace HiveMind.Server.Endpoints.ProgramStrategies;

public class GetAll
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", Handle).WithName("GetAllProgramStrategies");
    }

    public record ProgramStrategy(int Id, string Name, int AdvancedDays, bool Active, DateOnly? LastScheduledDate);
    public record GetAllProgramStrategiesResponse(List<ProgramStrategy> Strategies, int CurrentPage, int PageSize, int DataCount);

    public record Parameters(
       string? Search,
       int? Page,
       int? Count
    );

    public static Results<Ok<GetAllProgramStrategiesResponse>, NotFound> Handle(ProgramStrategyService programStrategy, [AsParameters] MediaParameters filters)
    {
        var strategies = programStrategy.GetAllProgramStrategies();

        var pageSize = filters.Count ?? 100;
        var currentPage = filters.Page ?? 1;
        var dataCount = strategies.Count();

        if (filters.Search != null && filters.Search != string.Empty) strategies = strategies.Where(x => x.Name.ToLower().Contains(filters.Search.ToLower()));

        var m = (pageSize * currentPage);
        strategies = strategies.Skip((pageSize * (currentPage - 1))).Take(pageSize);

        return TypedResults.Ok(new GetAllProgramStrategiesResponse(strategies.Select(x => new ProgramStrategy(x.Id, x.Name, x.AdvancedDays, x.Active, x.LastScheduleDate)).ToList(), currentPage, pageSize, dataCount));
    }
}
