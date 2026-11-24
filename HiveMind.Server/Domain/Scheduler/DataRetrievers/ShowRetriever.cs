using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Entities;
using HiveMind.Server.Services;

namespace HiveMind.Server.Domain.Scheduler.DataRetrievers;

public class ShowRetreiver: IDataRetriever
{
    public string ShowName { get; set; } = null!;

    public int? Season { get; set; }

    public int? Episode { get; set; }

    public bool UseScheduleStrategy { get; set; } = true;

    public RetreiverType RetrievalSetting { get; set; } = RetreiverType.Random;
    public IEnumerable<GenerationResultItem> Retrieve(GenerationContext context)
    {
        var results = new List<GenerationResultItem>();
        var queries = new List<String>();

        if(UseScheduleStrategy)
        {
            var rotationService = context.ServiceProvider.GetRequiredService<ProgramRotationService>();
            var mediaService = context.ServiceProvider.GetRequiredService<MediaItemService>();

            var rotation = rotationService.Get().Where(x => x.Name == ShowName).FirstOrDefault();

            var showMediaItems = mediaService.GetAllMediaItems()
                    .Where(x => x.Show!.Name == ShowName);
                    
            if(rotation != null)
            {
                var start = rotation.StartDate;
                
                // Limit our search to a specific season
                if(rotation.Season != null) showMediaItems = showMediaItems.Where(x => x.SeasonNumber == rotation.Season);

                showMediaItems = showMediaItems.OrderBy(x => x.EpisodeNumber)
                    .ThenBy(x => x.SeasonNumber);

                var iterationResults = CountIterations(start, context.SchedulingDate, rotation.Recurrence, rotation.Interval);

                if (iterationResults.isCurrentDate)
                {
                    var showMediaItem = showMediaItems.Skip(iterationResults.ticks).FirstOrDefault();

                    if(showMediaItem != null) return [new GenerationResultItem { MediaItem = showMediaItem, StartTime = 0, EndTime = showMediaItem.Duration }];
                }

                var possibleItems = showMediaItems.Take(iterationResults.ticks);
                var selectedItem = possibleItems.ToList()[Random.Shared.Next(possibleItems.Count())];
                return [new GenerationResultItem { MediaItem = selectedItem, StartTime = 0, EndTime = selectedItem.Duration }];
            }

            var mediaItem = showMediaItems.ToList()[Random.Shared.Next(showMediaItems.Count())];
            return [new GenerationResultItem { MediaItem = mediaItem, StartTime = 0, EndTime = mediaItem.Duration }];
        }
        

        queries.Insert(0, "Show, Equals, " + ShowName);

        if(Season.HasValue) queries.Insert(0, "SeasonNumber, Equals, " + Season.Value);
        if(Episode.HasValue) queries.Insert(0, "EpisodeNumber, Equals, " + Episode.Value);

        var media =  context.Retriever.GetMedia([.. queries], RetrievalSetting).FirstOrDefault();

        if (media != null) results.Add(new GenerationResultItem { MediaItem = media, StartTime = 0, EndTime = media.Duration });

        return results;
    }

    public record IterationResult(int ticks, bool isCurrentDate);

    public static IterationResult CountIterations(DateOnly start, DateOnly current, RecurrenceType recurrenceType, int interval)
    {
        if(recurrenceType == RecurrenceType.Daily)
        {
            double difference = Math.Abs(current.DayNumber - start.DayNumber);

            return new IterationResult((int)difference / interval, difference % interval == 0);
        }

        if(recurrenceType == RecurrenceType.Weekly)
        {
            double difference = Math.Abs(current.DayNumber - start.DayNumber);

            return new IterationResult((int)difference/ (interval * 7), difference % (interval * 7) == 0);
        }

        int months = 1;

        var currentDateToCheck = start;

        while(currentDateToCheck < current)
        {
            months += 1;
            currentDateToCheck.AddMonths(1);
        }

        return new IterationResult(months / (interval), current == currentDateToCheck);
    }

}