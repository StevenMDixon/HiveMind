using HiveMind.Server.Domain.Enums;
using HiveMind.Server.HostedServices;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints;

public class ImportStatus
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("import-status");
        endpoints.MapGet("/", Handle).WithName("GetImporterStatus");
    }

    public record ImportStatusResult(string Status, string Message);

    public static Results<Ok<ImportStatusResult>, NotFound> Handle(ImportStatusService importStatusService)
    {
        var currentStatus = importStatusService.GetStatus();

        return TypedResults.Ok(new ImportStatusResult(currentStatus.State.ToString(), currentStatus.Message ?? ""));
    }
}
