namespace HiveMind.Server.Domain.Scheduler;

public record ScheduleItemResult
(
    string logo,
    string path,
    string name,
    int duration,
    int startTime,
    int stopTime
);
