using HiveMind.Server.Domain.Enums;
using System.Text.Json.Serialization;

namespace HiveMind.Server.Domain.Scheduler.DataRetrievers;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(QueryRetreiver), "query")]
[JsonDerivedType(typeof(ServerRetreiver), "server")]
[JsonDerivedType(typeof(ShowRetreiver), "show")]
public interface IDataRetriever
{
    public abstract IEnumerable<GenerationResultItem> Retrieve(GenerationContext context); 
}