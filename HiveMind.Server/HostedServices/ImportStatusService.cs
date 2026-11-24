using HiveMind.Server.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace HiveMind.Server.HostedServices;

public class ImportStatusService(
    IHubContext<ImportHub, IImportClient> hubContext)
{
    public ImportStatusDto _current = new(
        ImportState.Idle,
        ""
    );

    public ImportStatusDto GetStatus() => _current;

    public async Task UpdateStatusAsync(ImportStatusDto status)
    {
        _current = status;

        await hubContext.Clients.All.ImportUpdated(status);
    }
}