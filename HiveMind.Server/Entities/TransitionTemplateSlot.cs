using HiveMind.Server.Domain.Enums;

namespace HiveMind.Server.Entities;

public class TransitionTemplateSlot
{
    public int TransitionTemplateSlotId { get; set; }
    public TransitionSlot Slot { get; set; }    
    public int TransitionTemplateId { get; set; }
    public int Index { get; set; } = 0;
}