using FluentValidation;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Schedules;

public class UpdateSchedule
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("/{id:int}", Handle)
            .WithRequestValidation<Validator>()
            .WithName("UpdateSchedule")
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

    public static Results<Ok, NotFound<string>, ValidationProblem> Handle(ScheduleService scheduleService, [FromRoute] int id, [FromBody] ScheduleRequest request)
    {
        var schedule = scheduleService.GetScheduleByID(id);

        if (schedule is not null)
        {
            schedule.ScheduleName = request.ScheduleName;
            schedule.ChannelId = request.ChannelId;
            schedule.StartTime = request.StartTime;
            scheduleService.Update(schedule);
            return TypedResults.Ok();
        }

        return TypedResults.NotFound($"A schedule with the ID: {id} was not found.");
    }
}
