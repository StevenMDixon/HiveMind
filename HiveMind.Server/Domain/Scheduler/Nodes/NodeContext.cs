using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Domain.Scheduler.Sources;
using HiveMind.Server.Entities;

namespace HiveMind.Server.Domain.Scheduler;

public class NodeContext
{
    public int? Duration { get; set; }
    public int UsedDuration { get; set; } = 0;
    public bool UseParentName {get; set;} = false;
    public string Name { get; set; } = string.Empty;
    public string Logo { get; set; } = string.Empty;
    public Dictionary<TransitionSlot, IDataSource> Sources { get; set; } = new Dictionary<TransitionSlot, IDataSource>();
    public Dictionary<TransitionType, List<TransitionTemplateSlot>> Transitions { get; set; } = new Dictionary<TransitionType, List<TransitionTemplateSlot>>();
    public MediaItem? Intro { get; set; }
    public MediaItem? Outro { get; set; }
}