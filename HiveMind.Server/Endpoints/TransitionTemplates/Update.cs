using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Entities;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static HiveMind.Server.Endpoints.ProgramStrategies.Update;

namespace HiveMind.Server.Endpoints.TransitionTemplates;

public class Update
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("/", Handle)
            .WithName("Update");
    }

    public record UpdateRequest(int id, string? Name, List<Slot>? Slots);

    public record Slot(int Id, TransitionSlot SlotType, int TransitionTemplateId, int Index);

    public static Results<Ok, NoContent, ValidationProblem> Handle(TransitionTemplateService transitionTemplateService, [FromBody] UpdateRequest updateRequest)
    {
        var template = transitionTemplateService.GetTransitionTemplateByID(updateRequest.id);

        if (template == null)
        {
            return TypedResults.NoContent();
        }

        template.Name = updateRequest.Name ?? template.Name;
        
        var itemsToUpdate = new List<(TransitionTemplateSlot, Slot)>();

        foreach (var slot in updateRequest.Slots ?? [])
        {
            var matchedItem = template?.Slots?.Where(x => x.Id == slot.Id).FirstOrDefault();

            if (matchedItem != null)
            {
                itemsToUpdate.Add((matchedItem, slot));
            }

            itemsToUpdate.Add((new TransitionTemplateSlot()
            {
                TransitionTemplateId = template!.Id,
                Slot = slot.SlotType,
                Index = slot.Index
            }, slot));
        }

        template.Slots = Helper.Resolve(itemsToUpdate, UpdateItem);

        transitionTemplateService.UpdateTransitionTemplate(template);

        return TypedResults.Ok();
    }

    private static TransitionTemplateSlot UpdateItem(TransitionTemplateSlot slot, Slot updatedSlot)
    {
        if (updatedSlot != null)
        {
            slot.Id = updatedSlot.Id;
            slot.TransitionTemplateId = updatedSlot.TransitionTemplateId;
            slot.Slot = updatedSlot.SlotType;
            slot.Index = updatedSlot.Index;
        }

        return slot;
    }
}
