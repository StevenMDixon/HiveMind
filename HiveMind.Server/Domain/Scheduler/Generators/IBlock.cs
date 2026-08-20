using System.Text.Json.Serialization;

namespace HiveMind.Server.Domain.Scheduler.Nodes;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "nodeType")]
[JsonDerivedType(typeof(BlockGenerator), "block")]
public interface IBlock 
{
    public abstract IEnumerable<GenerationResultItem> Generate(GenerationContext context);
}
