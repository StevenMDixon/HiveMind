using HiveMind.Server.Domain.Scheduler.DataProvider;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace HiveMind.Server.Endpoints.Scheduler;

public class GetLineupResults
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", Handle)
            .WithName("GetLineupResults");
    }

    public record Request(JsonElement Nodes);

    public record Response();

    public static Results<Ok, NoContent, ValidationProblem> Handle(IServiceProvider services, ScheduleDataProvider scheduleDataProvider, [FromBody] Request request)
    {
        var scheduler = new HiveMind.Server.Domain.Scheduler.Scheduler(services, scheduleDataProvider);

        var results = scheduler.GenerateTestSchedule(request.Nodes.GetRawText()).Result;

        return TypedResults.Ok();
    }
}
