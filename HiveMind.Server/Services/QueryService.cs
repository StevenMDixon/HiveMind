using HiveMind.Server.Entities;
using Microsoft.EntityFrameworkCore;
namespace HiveMind.Server.Services;
using HiveMind.Server.QueryEngine;

public class QueryService: BaseService
{
    public QueryService(sqliteDBContext context) : base(context) { }

    public IEnumerable<Query> GetAllQueries()
    {
        return _context.Queries.Include(c => c.Filters);
    }

    public Query AddQuery(Query query)
    {
        _context.Queries.Add(query);
        _context.SaveChanges();
        return query;
    }

    public Query? GetQueryByID(int id)
    {
        return _context.Queries.Include(x => x.Filters).FirstOrDefault(x => x.QueryId == id);
    }

    public void Update(Query query)
    {
        _context.Queries.Update(query);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var query = _context.Queries.Find(id);
        if (query != null)
        {
            _context.Queries.Remove(query);
            _context.SaveChanges();
        }
    }

    public ICollection<MediaItem> GetMediaItemsByQueryId(int queryId)
    {
        var query = _context.Queries.Include(q => q.Filters).FirstOrDefault(q => q.QueryId == queryId);
        
        if (query == null || query.Filters == null)
        {
            return Array.Empty<MediaItem>();
        }

        var queryRequest = new QueryRequest
        {
            Filters = query.Filters.Select(f => new FilterRule(f.Field, f.Operator, f.Value)).ToList()
        };

        var mediaItemsQuery = _context.MediaItems.AsQueryable();
        mediaItemsQuery = MediaQueryBuilder.Apply(mediaItemsQuery, queryRequest);

        return mediaItemsQuery.ToList();
    }
}