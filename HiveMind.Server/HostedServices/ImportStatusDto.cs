namespace HiveMind.Server.HostedServices;

public enum ImportState
{
    Idle,
    Running,
    Processing,
    Completed,
    Failed
}

public record ImportStatusDto(
    ImportState State,
    string? Message
);