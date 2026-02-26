using HiveMind.Server.Entities;
using Microsoft.EntityFrameworkCore;
namespace HiveMind.Server.Services;

public class CollectionService: BaseService
{
    public CollectionService(sqliteDBContext context) : base(context) { }

    public IEnumerable<Collection> GetAllCollections()
    {
        return _context.Collections.Include(c => c.Queries);
    }

    public void AddCollection(Collection collection)
    {
        _context.Collections.Add(collection);
        _context.SaveChanges();
    }

    public Collection? GetCollectionByID(int id)
    {
        return _context.Collections.Find(id);
    }

    public void Update(Collection collection)
    {
        _context.Collections.Update(collection);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var collection = _context.Collections.Find(id);
        if (collection != null)
        {
            _context.Collections.Remove(collection);
            _context.SaveChanges();
        }
    }
}