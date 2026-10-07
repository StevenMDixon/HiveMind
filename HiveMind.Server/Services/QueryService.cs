using HiveMind.Server.Entities;
using Microsoft.EntityFrameworkCore;
namespace HiveMind.Server.Services;

using HiveMind.Server.Domain.Enums;
using HiveMind.Server.QueryEngine;

public class QueryService(SqliteDBContext context) : BaseService<Query>(context)
{
    public IEnumerable<Query> GetAllQueries()
    {
        return Get();
    }

    public Query AddQuery(Query query)
    {
        return Create(query);
    }

    public Query? GetQueryByID(int id)
    {
        return Get().FirstOrDefault(x => x.Id == id);
    }

    public Query? GetQueryByName(string name)
    {
        return Get().FirstOrDefault(x => x.Name == name);
    }

    public void Update(Query query)
    {
        Console.WriteLine(_context.Entry(query).State);

        Update(query);
    }

    public void Delete(int id)
    {
        var query = Get().FirstOrDefault(x => x.Id == id);
        if (query != null)
        {
            Delete(query);
        }
    }

    public ICollection<MediaItem> GetMediaItemsByQueryId(int queryId, List<(string, string, string)>? customFilters = null)
    {
        var query = Get().Include(x => x.Filters).FirstOrDefault(q => q.Id == queryId);
        
        if (query == null || query.Filters == null)
        {
            return Array.Empty<MediaItem>();
        }

        var queryRequest = new QueryRequest
        {
            Filters = [.. query.Filters.Select(f => new FilterRule(f.Field, f.Operator, f.Value))]
        };

        if(customFilters != null && customFilters.Count > 0)
        {
            queryRequest.Filters.AddRange([.. customFilters.Select(f => new FilterRule(Enum.Parse<QueryEnums.QueryAllowedFields>(f.Item1), Enum.Parse<QueryEnums.QueryAllowedOperators>(f.Item2), f.Item3))]);
        }

        var mediaItemsQuery = _context.MediaItems.AsQueryable();
        mediaItemsQuery = MediaQueryBuilder.Apply(mediaItemsQuery, queryRequest);

        return [.. mediaItemsQuery];
    }

    public ICollection<MediaItem> GetMediaItemsByQueryGroup(ICollection<int> queryIds, List<(string, string, string)>? customFilters = null)
    {
        var queries = _context.Queries.Include(q => q.Filters).Where(q => queryIds.Contains(q.Id)).ToList();

        if (queries == null || !(queries.Count > 0))
        {
            return Array.Empty<MediaItem>();
        }

        var filtersList = queries.Select(q => q.Filters?.Select(f => new FilterRule(f.Field, f.Operator, f.Value)).ToList() ?? []).ToList();

        if (customFilters != null)
        {
            filtersList.AddRange(customFilters.Select(f => new FilterRule(Enum.Parse<QueryEnums.QueryAllowedFields>(f.Item1), Enum.Parse<QueryEnums.QueryAllowedOperators>(f.Item2), f.Item3)).ToList());
        }

        var queryUnionRequest = new QueryUnionRequest() { Queries = filtersList};

        var mediaItemsQuery = MediaQueryBuilder.ApplyWithUnion(_context.MediaItems, queryUnionRequest);

        return [.. mediaItemsQuery];
    }

    public ICollection<MediaItem> GetMediaItemsByQueries(List<(string, string, string)> customFilters)
    {
        var queries = new List<List<FilterRule>>();

        queries.AddRange(customFilters
            .Select(f => new FilterRule(Enum.Parse<QueryEnums.QueryAllowedFields>(f.Item1), Enum.Parse<QueryEnums.QueryAllowedOperators>(f.Item2), f.Item3)).ToList());

        var queryUnionRequest = new QueryUnionRequest() { Queries = queries  };

        var mediaItemsQuery = MediaQueryBuilder.ApplyWithUnion(_context.MediaItems, queryUnionRequest);

        return [.. mediaItemsQuery];
    }

    public void Create(List<Query> queries)
    {
        _context.Queries.AddRange(queries);
        _context.SaveChanges();
    }
}