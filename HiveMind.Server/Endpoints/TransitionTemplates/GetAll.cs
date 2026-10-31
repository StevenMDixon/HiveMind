using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HiveMind.Server.Endpoints.TransitionTemplates;

public class GetAll
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", Handle)
            .WithName("GetAllTransitionTemplates");
    }

    public record Transitions(int Id, string Name);

    public record Response(List<Transitions> Transitions);

    public record Parameters(
       string? Search,
       int? Page,
       int? Count
    );

    public static Results<Ok<Response>, NoContent, ValidationProblem> Handle(TransitionTemplateService service, [AsParameters] Parameters filters)
    {
        var transitions = service.GetAllTransitionTemplates();

        var pageSize = filters.Count ?? 100;
        var currentPage = filters.Page ?? 1;
        var dataCount = transitions.Count();

        if (filters.Search != null && filters.Search != string.Empty) transitions = transitions.Where(x => x.Name.ToLower().Contains(filters.Search.ToLower()));

        return TypedResults.Ok(new Response(transitions.Select(x => new Transitions(x.Id, x.Name)).ToList()));
    }
}
