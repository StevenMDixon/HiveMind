using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HiveMind.Server.Endpoints.TransitionTemplates;

public class GetTransitionTemplates
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", Handle).WithName("GetTransitionTemplates");
    }

    public record TransitionTemplate(int Id, string Name, List<TransitionTemplateSlot> Slots);

    public record TransitionTemplateSlot(int Id, TransitionSlot SlotType, int TransitionTemplateId, int Index);

    public record TransitionTemplateResponse(ICollection<TransitionTemplate> TransitionTemplates);

    public static Results<Ok<TransitionTemplateResponse>, NotFound> Handle(TransitionTemplateService transitionTemplateService)
    {
        var transitionTemplates = transitionTemplateService.GetAllTransitionTemplates();

        var templates = transitionTemplates.Select(template => new TransitionTemplate(template.Id, template.Name, template.Slots.Select(x => new TransitionTemplateSlot(x.Id, x.Slot, x.TransitionTemplateId, x.Index)).ToList()));

        return TypedResults.Ok(new TransitionTemplateResponse(templates.ToList()));
    }
}
