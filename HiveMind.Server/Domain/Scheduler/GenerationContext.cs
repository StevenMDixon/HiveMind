using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Entities;

namespace HiveMind.Server.Domain.Scheduler;

public class GenerationContext()
{
    public DateOnly SchedulingDate {get; set;}
    public MediaItemRetriever Retriever { get; set; } = null!;
    public IServiceProvider ServiceProvider { get; set; } = null!;
    public List<SourceItem> PromoQueries { get; set; } = [];
    public Dictionary<string, string> Settings { get; set; } = [];
}