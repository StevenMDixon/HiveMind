using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Entities;

namespace HiveMind.Server.Domain.Scheduler;

public class GenerationContext()
{
    public MediaItemRetriever Retriever { get; set; } = null!;
    public IServiceScope Scope { get; set; } = null!;
    public List<BlockContext> BlockContext { get; set; } = new List<BlockContext>();
    public List<(int, string)> PromoQueries { get; set; } = new List<(int, string)>();
    public Dictionary<string, string> Settings { get; set; } = new Dictionary<string, string>();
}

public class BlockContext
{
    // Add properties and methods for BlockContext here
    public List<String> Shows { get; set; } = new List<String>();


}