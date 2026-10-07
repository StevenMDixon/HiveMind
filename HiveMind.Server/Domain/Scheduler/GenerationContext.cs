using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Entities;

namespace HiveMind.Server.Domain.Scheduler;

public class GenerationContext()
{
    public MediaItemRetriever Retriever { get; set; } = null!;
    public IServiceProvider ServiceProvider { get; set; } = null!;
    public List<BlockContext> BlockContext { get; set; } = [];
    public List<SourceItem> PromoQueries { get; set; } = [];
    public Dictionary<string, string> Settings { get; set; } = [];
}

public class BlockContext
{
    public List<String> Shows { get; set; } = [];
    public string Name { get; set; } = string.Empty;
    public string Logo { get; set; } = string.Empty;
    public Dictionary<TransitionSlot, SourceItem> Sources { get; set; } = [];
    public Dictionary<TransitionType, List<TransitionTemplateSlot>> Transitions { get; set; } = [];
    public List<ProgramContext> ProgramContexts { get; set; } = [];
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