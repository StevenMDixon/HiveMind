namespace HiveMind.Server.Domain.Scheduler;

public record ScheduleItemResult
(
    string Logo,
    string Path,
    string Name,
    int Duration,
    int StartTime,
    int StopTime,
    bool HasBlackBars,
    string Resolution,
    string Rating
);
