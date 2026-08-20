using System.Text.Json.Serialization;

namespace HiveMind.Server.Endpoints.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SelectionPolicy
{
    Single,
    Fill
}
