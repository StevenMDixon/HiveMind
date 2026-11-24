using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Domain.Scheduler.Sources;

namespace HiveMind.Server.Domain.Scheduler.DataRetrievers;

public class QueryRetreiver: IDataRetriever
{
    public IDataSource Query { get; set; } = null!;
    public IEnumerable<GenerationResultItem> Retrieve(GenerationContext context)
    {
        var query = Query.GetQuery(context.ScheduleDataProvider.Queries);

        if(query != null)
        {
            var possibleMedia = context.ScheduleDataProvider.Queries.GetMediaItemsByQuery(query);
            
            var media = possibleMedia.OrderBy(r => Guid.NewGuid()).FirstOrDefault();

            if(media != null) return [new GenerationResultItem { MediaItem = media, StartTime = 0, EndTime = media.Duration }];
        }
        
        return [];
    }
}