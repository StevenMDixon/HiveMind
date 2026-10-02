using System.Text.Json.Serialization;

namespace HiveMind.Server.Domain.Scheduler.Nodes;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "nodeType")]
[JsonDerivedType(typeof(BlockNode), "block")]
[JsonDerivedType(typeof(EventNode), "event")]

[JsonDerivedType(typeof(ProgramNode), "program")]
[JsonDerivedType(typeof(EventSegementNode), "segment")]

[JsonDerivedType(typeof(SplitterNode), "split")]
[JsonDerivedType(typeof(RandomNode), "random")]
[JsonDerivedType(typeof(BreakNode), "break")]

[JsonDerivedType(typeof(MediaShowNode), "show")]
[JsonDerivedType(typeof(MediaServerNode), "server")]
[JsonDerivedType(typeof(MediaQueryNode), "query")]
public interface INode
{
    public abstract IEnumerable<GenerationResultItem> Generate(GenerationContext context);
}
