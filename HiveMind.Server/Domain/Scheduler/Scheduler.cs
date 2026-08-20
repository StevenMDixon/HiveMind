using HiveMind.Server.Domain.Scheduler.Nodes;
using HiveMind.Server.Entities;
using HiveMind.Server.Services;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HiveMind.Server.Domain.Scheduler;

public class Scheduler
{
    private readonly IServiceScope _scope;

    private readonly MediaItemRetriever _retriever;

    public Scheduler(IServiceScope scope)
    {
        _scope = scope;
        _retriever = new MediaItemRetriever(_scope.ServiceProvider.GetRequiredService<QueryService>());
    }


    public async Task GenerateSchedule(int lineUpId, int programStrategyID, DateOnly scheduledDate)
    {
        var lineupService = _scope.ServiceProvider.GetRequiredService<LineupService>();

        if (lineUpId == 0) return;
         
        var lineup = lineupService.GetLineupByID(lineUpId);

        var scheduleNodes = new List<IBlock>();

        if (lineup != null)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            options.Converters.Add(new JsonStringEnumConverter());

            scheduleNodes.AddRange(JsonSerializer.Deserialize<List<IBlock>>(lineup.JsonData, options) ?? new List<IBlock>());
        }

        var scheduleContext = new ScheduleContext() { date = scheduledDate };

        if (scheduleNodes.Any())
        {
            var result = ExecuteSchedule(scheduleNodes, scheduleContext);

            if (result != null)
            {
               var sheduleResultLocation = WriteOutSchedule(programStrategyID.ToString(), result.outputLoc, scheduledDate.ToString("MMddyyyy", CultureInfo.InvariantCulture), JsonSerializer.Serialize(result));
                var scheduleResult = new SchedulingResult()
                {
                    Date = scheduledDate,
                    Path = sheduleResultLocation,
                    ProgramStrategyId = programStrategyID
                };

                var scheduleService = _scope.ServiceProvider.GetRequiredService<ScheduleService>();

                scheduleService.Create(scheduleResult);
            }
        }
    }

    public record GenerationResult(string outputLoc, List<ScheduleItemResult> items, TimeOnly startTime);

    public GenerationResult ExecuteSchedule(List<IBlock> scheduleNodes, ScheduleContext scheduleData)
    {
        var settingService = _scope.ServiceProvider.GetRequiredService<SettingsService>();


        var generationContext = new GenerationContext
        {
            Retriever = _retriever,
            Scope = _scope,
            Settings = settingService.GetAllSettings().ToDictionary(x => x.Name, x => x.Value),
        };

        var results = new List<GenerationResultItem>();

        for (var i = scheduleNodes.Count() - 1; i >= 0 ; i--)
        {
            var node = scheduleNodes[i];

            var blockContext = new BlockContext();

            generationContext.BlockContext.Add(blockContext);

            results.AddRange(node.Generate(generationContext));
        }

        var outPutLocation = generationContext.Settings["Export_Location"];

        var generationResult = new GenerationResult(outPutLocation, results.Select(x => new ScheduleItemResult("", x.MediaItem.FilePath, x.MediaItem.Title, x.Duration(), x.StartTime, x.EndTime)).ToList(), TimeOnly.MinValue);

        return generationResult;
    }

    public string WriteOutSchedule(string fileName, string path, string programFolder, string json)
    {   
        var defaultPath = path == "/" ? "./" : Path.GetFullPath(path);

        string fullPath = Path.Combine(defaultPath, programFolder, fileName + ".json") ?? "./";
        string directory = Path.GetDirectoryName(fullPath) ?? "./";
        var outDir = Directory.CreateDirectory(directory);
        File.WriteAllText(fullPath, json);

        return fullPath;
    }
}