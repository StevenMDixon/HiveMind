using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Domain.Scheduler.DataProvider;
using HiveMind.Server.Domain.Scheduler.Sources;
using HiveMind.Server.Entities;

namespace HiveMind.Server.Domain.Scheduler;

public class GenerationContext()
{
    public DateOnly SchedulingDate {get; set;}
    public ScheduleDataProvider ScheduleDataProvider { get; set; } = null!;
    public List<IDataSource> PromoQueries { get; set; } = [];
    public Dictionary<string, string> Settings { get; set; } = [];
}