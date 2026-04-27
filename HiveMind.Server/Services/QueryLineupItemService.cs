using HiveMind.Server.Entities;

namespace HiveMind.Server.Services;

public class QueryLineupItemService : BaseService
{
    public QueryLineupItemService(sqliteDBContext context) : base(context) { }

    public IEnumerable<QueryLineupItem> GetAllQueryLineupItems()
    {
        return _context.QueryLineupItems;
    }

    public IEnumerable<QueryLineupItem> GetQueryLineupItemsByQueryLineupID(int queryLineupItemID)
    {
        return _context.QueryLineupItems
            .Where(qli => qli.QueryLineupItemId == queryLineupItemID);
    }

    public IEnumerable<QueryLineupItem> GetQueryLineupItemsByLineupItemID(int lineupItemID)
    {
        return _context.QueryLineupItems
            .Where(qli => qli.LineupItemId == lineupItemID);
    }

    public void AddQueryLineupItem(QueryLineupItem queryLineupItem)
    {
        _context.QueryLineupItems.Add(queryLineupItem);
        _context.SaveChanges();
    }

    public QueryLineupItem? GetQueryLineupItemByID(int id)
    {
        return _context.QueryLineupItems.Find(id);
    }

    public QueryLineupItem Update(QueryLineupItem queryLineupItem)
    {
        _context.QueryLineupItems.Update(queryLineupItem);
        _context.SaveChanges();

        return queryLineupItem;
    }

    public void Delete(int id)
    {
        var queryLineupItem = _context.QueryLineupItems.Find(id);
        if (queryLineupItem != null)
        {
            _context.QueryLineupItems.Remove(queryLineupItem);
            _context.SaveChanges();
        }
    }
}
