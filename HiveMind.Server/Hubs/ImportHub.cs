using HiveMind.Server.HostedServices;
using Microsoft.AspNetCore.SignalR;

namespace HiveMind.Server.Hubs;

public interface IImportClient
{
    Task ImportUpdated(ImportStatusDto status);
}

public class ImportHub : Hub<IImportClient>
{
    
}