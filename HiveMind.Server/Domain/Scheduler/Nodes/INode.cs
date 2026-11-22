using System.Text.Json.Serialization;

namespace HiveMind.Server.Domain.Scheduler.Nodes;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "nodeType")]
[JsonDerivedType(typeof(BlockNode), "block")]
[JsonDerivedType(typeof(BlockSegmentNode), "segment")]
public interface INode
{
    public abstract IEnumerable<GenerationResultItem> Generate(GenerationContext context, NodeContext? nodeContext);
}
