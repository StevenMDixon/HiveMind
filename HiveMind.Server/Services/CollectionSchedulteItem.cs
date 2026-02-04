using HiveMind.Server.Entities;

namespace HiveMind.Server.Services;

public class CollectionSchedulteItemService : BaseService
{
    public CollectionSchedulteItemService(sqliteDBContext context) : base(context) { }

    public IEnumerable<CollectionScheduleItem> GetAllCollectionScheduleItems()
    {
        return _context.CollectionScheduleItems;
    }

    public IEnumerable<CollectionScheduleItem> GetCollectionScheduleItemsByCollectionScheduleID(int collectionScheduleItemID)
    {
        return _context.CollectionScheduleItems
            .Where(csi => csi.CollectionScheduleItemId == collectionScheduleItemID);
    }

    public IEnumerable<CollectionScheduleItem> GetCollectionScheduleItemsByScheduleItemID(int scheduleItemID)
    {
        return _context.CollectionScheduleItems
            .Where(csi => csi.ScheduleItemId == scheduleItemID);
    }

    public void AddCollectionScheduleItem(CollectionScheduleItem collectionScheduleItem)
    {
        _context.CollectionScheduleItems.Add(collectionScheduleItem);
        _context.SaveChanges();
    }

    public CollectionScheduleItem? GetCollectionScheduleItemByID(int id)
    {
        return _context.CollectionScheduleItems.Find(id);
    }

    public void Update(CollectionScheduleItem collectionScheduleItem)
    {
        _context.CollectionScheduleItems.Update(collectionScheduleItem);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var collectionScheduleItem = _context.CollectionScheduleItems.Find(id);
        if (collectionScheduleItem != null)
        {
            _context.CollectionScheduleItems.Remove(collectionScheduleItem);
            _context.SaveChanges();
        }
    }
}
