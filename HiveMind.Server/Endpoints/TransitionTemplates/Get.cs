using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.TransitionTemplates;

public class Get
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:int}", Handle).WithName("GetTransitionTemplatesById");
    }

    public record TransitionTemplate(int Id, string Name, bool MatchShowBumps, List<TransitionTemplateSlot> Slots);

    public record TransitionTemplateSlot(int Id, TransitionSlot SlotType, int TransitionTemplateId, int Index);


    public static Results<Ok<TransitionTemplate>, NotFound> Handle(TransitionTemplateService transitionTemplateService, [FromRoute] int Id)
    {
        var transitionTemplate = transitionTemplateService.GetTransitionTemplateByID(Id);

        if (transitionTemplate == null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(new TransitionTemplate(transitionTemplate.Id, transitionTemplate.Name, transitionTemplate.MatchShowBumps, transitionTemplate.Slots.Select(x => new TransitionTemplateSlot(x.Id, x.Slot, x.TransitionTemplateId, x.Index)).ToList()));
    }
}
