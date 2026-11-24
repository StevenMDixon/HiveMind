using HiveMind.Server.Entities;

namespace HiveMind.Server.Services;

public interface IQueryService: IBaseService<Query>
{
    public Query? GetQueryByID(int id);
    
    public Query? GetQueryByName(string name);

    public ICollection<MediaItem> GetMediaItemsByQueryId(int queryId, List<(string, string, string)>? customFilters = null);
    public ICollection<MediaItem> GetMediaItemsByQueryGroup(ICollection<int> queryIds, List<(string, string, string)>? customFilters = null);
    public ICollection<MediaItem> GetMediaItemsByQueries(List<(string, string, string)> customFilters);
    public ICollection<MediaItem> GetMediaItemsByQuery(Query query);
}