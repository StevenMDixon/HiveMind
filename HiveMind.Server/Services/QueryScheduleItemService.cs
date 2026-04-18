using HiveMind.Server.Entities;

namespace HiveMind.Server.Services;

public class QueryScheduleItemService : BaseService
{
    public QueryScheduleItemService(sqliteDBContext context) : base(context) { }

    public IEnumerable<QueryScheduleItem> GetAllQueryScheduleItems()
    {
        return _context.QueryScheduleItems;
    }

    public IEnumerable<QueryScheduleItem> GetQueryScheduleItemsByQueryScheduleID(int queryScheduleItemID)
    {
        return _context.QueryScheduleItems
            .Where(qsi => qsi.QueryScheduleItemId == queryScheduleItemID);
    }

    public IEnumerable<QueryScheduleItem> GetQueryScheduleItemsByScheduleItemID(int scheduleItemID)
    {
        return _context.QueryScheduleItems
            .Where(qsi => qsi.ScheduleItemId == scheduleItemID);
    }

    public void AddQueryScheduleItem(QueryScheduleItem queryScheduleItem)
    {
        _context.QueryScheduleItems.Add(queryScheduleItem);
        _context.SaveChanges();
    }

    public QueryScheduleItem? GetQueryScheduleItemByID(int id)
    {
        return _context.QueryScheduleItems.Find(id);
    }

    public QueryScheduleItem Update(QueryScheduleItem queryScheduleItem)
    {
        _context.QueryScheduleItems.Update(queryScheduleItem);
        _context.SaveChanges();

        return queryScheduleItem;
    }

    public void Delete(int id)
    {
        var queryScheduleItem = _context.QueryScheduleItems.Find(id);
        if (queryScheduleItem != null)
        {
            _context.QueryScheduleItems.Remove(queryScheduleItem);
            _context.SaveChanges();
        }
    }
}
