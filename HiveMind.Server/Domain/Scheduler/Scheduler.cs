using HiveMind.Server.Domain.Scheduler.Nodes;
using HiveMind.Server.Entities;
using HiveMind.Server.Services;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HiveMind.Server.Domain.Scheduler;

public class Scheduler
{
    private readonly IServiceProvider _serviceProvider;

    private readonly MediaItemRetriever _retriever;

    private readonly JsonSerializerOptions options = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public Scheduler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _retriever = new MediaItemRetriever(_serviceProvider.GetRequiredService<QueryService>());
    }

    public async Task<GenerationResult> GenerateTestSchedule(string jsonData)
    {
        var scheduleContext = new ScheduleContext() { Date = DateOnly.FromDateTime(DateTime.Today), ProgramId = 0 };

        options.Converters.Add(new JsonStringEnumConverter());

        var scheduleNodes = JsonSerializer.Deserialize<List<INode>>(jsonData, options) ?? [.. new List<INode>()];

        var result = ExecuteSchedule(scheduleNodes, scheduleContext);

        return result;
    }

    public async Task GenerateSchedule(int lineUpId, int programStrategyID, DateOnly scheduledDate)
    {
        if (lineUpId == 0) return;
        
        var lineupService = _serviceProvider.GetRequiredService<LineupService>();
         
        var lineup = lineupService.GetLineupByID(lineUpId);

        var scheduleNodes = new List<INode>();

        if (lineup != null)
        {
            options.Converters.Add(new JsonStringEnumConverter());

            scheduleNodes.AddRange(JsonSerializer.Deserialize<List<INode>>(lineup.JsonData, options) ?? [.. new List<INode>()]);
        }

        var scheduleContext = new ScheduleContext() { Date = scheduledDate, ProgramId = programStrategyID };

        if (scheduleNodes.Count != 0)
        {
            var result = ExecuteSchedule(scheduleNodes, scheduleContext);

            if (result != null)
            {
               var sheduleResultLocation = WriteOutSchedule(scheduledDate.ToString("MMddyyyy", CultureInfo.InvariantCulture), result.OutputLoc, programStrategyID.ToString(), JsonSerializer.Serialize(result));
                var scheduleResult = new SchedulingResult()
                {
                    Date = scheduledDate,
                    Path = sheduleResultLocation,
                    ProgramStrategyId = programStrategyID
                };

                var scheduleService = _serviceProvider.GetRequiredService<ScheduleService>();

                scheduleService.Create(scheduleResult);
            }
        }
    }

    public record GenerationResult(string OutputLoc, List<ScheduleItemResult> Items, TimeOnly StartTime);

    public GenerationResult ExecuteSchedule(List<INode> scheduleNodes, ScheduleContext scheduleData)
    {
        var settingService = _serviceProvider.GetRequiredService<SettingsService>();

        var generationContext = new GenerationContext
        {
            Retriever = _retriever,
            ServiceProvider = _serviceProvider,
            Settings = settingService.GetAllSettings().ToDictionary(x => x.Name, x => x.Value),
            PromoQueries = GetUpComingEventPromos(scheduleData.ProgramId)
        };

        var results = new List<GenerationResultItem>();

        for (var i = scheduleNodes.Count - 1; i >= 0 ; i--)
        {
            var node = scheduleNodes[i];

            var blockContext = new BlockContext();

            generationContext.BlockContext.Add(blockContext);

            results.InsertRange(0, node.Generate(generationContext));
        }

        var outPutLocation = generationContext.Settings["Export_Location"];

        var generationResult = new GenerationResult(outPutLocation, [.. results.Select(x => new ScheduleItemResult("", x.MediaItem.FilePath, x.MediaItem.Title, x.Duration(), x.StartTime, x.EndTime, x.MediaItem.HasBlackBars, x.MediaItem.Resolution, x.MediaItem?.Show?.Rating.ToString() ?? ""))], TimeOnly.MinValue);

        return generationResult;
    }

    private List<SourceItem> GetUpComingEventPromos(int programStrategyID)
    {
        var currentDate = DateTime.Today;

        var programEventService = _serviceProvider.GetRequiredService<ProgramEventService>();
        var upcomingEvents = programEventService.GetUpComingEvents(programStrategyID, currentDate);

        return [.. upcomingEvents.Select(e => new SourceItem() {Type = SourceType.Id, Value = e.QueryId.ToString()})];
    }

    public string WriteOutSchedule(string fileName, string path, string programFolder, string json)
    {   
        var defaultPath = path == "/" ? "./" : Path.GetFullPath(path);

        string fullPath = Path.Combine(defaultPath, programFolder, fileName + ".json") ?? "./";
        string directory = Path.GetDirectoryName(fullPath) ?? "./";
        Directory.CreateDirectory(directory);
        File.WriteAllText(fullPath, json);

        return fullPath;
    }
}