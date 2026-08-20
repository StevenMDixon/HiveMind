using System.Text.Json.Serialization;

namespace HiveMind.Server.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SplitterStrategy
{
    Every,
    ShowDefined,
    FixedCount
}
