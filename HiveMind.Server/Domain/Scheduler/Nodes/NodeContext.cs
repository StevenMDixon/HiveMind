using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Entities;

namespace HiveMind.Server.Domain.Scheduler;

public class NodeContext
{
    public int? Duration { get; set; }
    public int usedDuration { get; set; } = 0;
    public int segments {get; set;} = 0; 
    public bool UseParentName {get; set;} = false;
    public string Name { get; set; } = string.Empty;
    public string Logo { get; set; } = string.Empty;
    public Dictionary<TransitionSlot, SourceItem> Sources { get; set; } = [];
    public Dictionary<TransitionType, List<TransitionTemplateSlot>> Transitions { get; set; } = [];
    public MediaItem? Intro { get; set; }
    public MediaItem? Outro { get; set; }
}