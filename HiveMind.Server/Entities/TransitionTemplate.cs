namespace HiveMind.Server.Entities;

public class TransitionTemplate
{
    public int TransitionTemplateId { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool MatchShowBumps { get; set; } = true;
    public ICollection<TransitionTemplateSlot> Slots { get; set; } = [];
}
