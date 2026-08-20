using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Entities;
using HiveMind.Server.Services;

namespace HiveMind.Server.Domain.Scheduler;

public class MediaItemRetriever
{
    private readonly QueryService _QueryService;
    public MediaItemRetriever(QueryService queryService)
    {
        _QueryService = queryService;
    }

    public List<MediaItem> GetMedia(int queryId, RetreiverType retrieverType, int count = 1, List<(string, string, string)>? customFilters = null)
    {
        switch (retrieverType)
        {
            case RetreiverType.Random:
            var query = _QueryService.GetMediaItemsByQueryId(queryId, customFilters);
            return query.OrderBy(r => Guid.NewGuid()).Take(count).ToList();
            case RetreiverType.Sequential:
                return _QueryService.GetMediaItemsByQueryId(queryId, customFilters).Take(count).ToList();
            case RetreiverType.Shuffle:
                return _QueryService.GetMediaItemsByQueryId(queryId, customFilters).Take(count).ToList();   
            default:
            return new List<MediaItem>();
        }
    }

    public List<MediaItem> GetMedia(string[] queries, RetreiverType retrieverType, int count = 1, List<(string, string, string)>? customFilters = null)
    {
        var query = new List<(string, string, string)>();
    
        foreach(var q in queries)
        {
            var splitQ = q.Split(", ");
            query.Add((splitQ[0], splitQ[1], splitQ[2]));
        }

        if(customFilters != null)
        {
            query.AddRange(customFilters);
        }

        switch (retrieverType)
        {
            case RetreiverType.Random:
                var results = _QueryService.GetMediaItemsByQueries(query);
                return results.OrderBy(r => Guid.NewGuid()).Take(count).ToList();
            case RetreiverType.Sequential:
                return _QueryService.GetMediaItemsByQueries(query).Take(count).ToList();
            case RetreiverType.Shuffle:
                return _QueryService.GetMediaItemsByQueries(query).Take(count).ToList();
            default:
                return new List<MediaItem>();
        }
    }
}
