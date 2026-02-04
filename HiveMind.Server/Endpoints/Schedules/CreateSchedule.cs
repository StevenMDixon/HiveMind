using FluentValidation;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Schedules;

public class CreateSchedule
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", Handle)
            .WithRequestValidation<Validator>()
            .WithName("CreateSchedule")
            .ProducesValidationProblem();
    }

    public class Validator : AbstractValidator<ScheduleRequest>
    {
        public Validator()
        {
            RuleFor(x => x.ScheduleName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.ChannelId).GreaterThan(0);
        }
    }

    public record ScheduleRequest(string ScheduleName, int ChannelId, TimeOnly StartTime);

    public static Results<Ok, NoContent, ValidationProblem> Handle(ScheduleService scheduleService, [FromBody] ScheduleRequest request)
    {
        var newSchedule = new Entities.Schedule
        {
            ScheduleName = request.ScheduleName,
            ChannelId = request.ChannelId,
            StartTime = request.StartTime
        };

        scheduleService.AddSchedule(newSchedule);

        return TypedResults.NoContent();
    }
}
