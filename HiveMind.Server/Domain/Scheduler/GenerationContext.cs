using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Entities;

namespace HiveMind.Server.Domain.Scheduler;

public class GenerationContext()
{
    public MediaItemRetriever Retriever { get; set; } = null!;
    public IServiceScope Scope { get; set; } = null!;
    public List<BlockContext> BlockContext { get; set; } = new List<BlockContext>();
    public List<SourceItem> PromoQueries { get; set; } = new List<SourceItem>();
    public Dictionary<string, string> Settings { get; set; } = new Dictionary<string, string>();
}

public class BlockContext
{
    public List<String> Shows { get; set; } = new List<String>();
    public string Name { get; set; } = string.Empty;
    public string Logo { get; set; } = string.Empty;
    public Dictionary<TransitionSlot, SourceItem> Sources { get; set; } = new Dictionary<TransitionSlot, SourceItem>();
    public Dictionary<TransitionType, List<TransitionTemplateSlot>> Transitions { get; set; } = new Dictionary<TransitionType, List<TransitionTemplateSlot>>();
    public List<ProgramContext> ProgramContexts { get; set; } = new List<ProgramContext>();
    public MediaItem? Intro { get; set; }
    public MediaItem? Outro { get; set; }
    public int ProgramsNodeCount { get; set; } = 0;
}

public class ProgramContext
{
    public int Duration { get; set; }
    public int RemainingDuration { get; set; }
    public bool UseBlockName { get; set; } = false;
    public string ProgramName { get; set; } = string.Empty;
}