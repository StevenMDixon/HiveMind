using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using HiveMind.Server.Entities;

namespace HiveMind.Server.Endpoints.TransitionTemplates;

public class Create
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", Handle)
            .WithName("Create");
    }

    public record CreateRequest(string templateName);

    public static Results<Ok, NoContent, ValidationProblem> Handle(TransitionTemplateService transitionTemplateService, [FromBody] CreateRequest createRequest)
    {
        transitionTemplateService.CreateTransitionTemplate(new TransitionTemplate() { Name = createRequest.templateName });
        return TypedResults.Ok();
    }
}
