using HiveMind.Server.Entities;

namespace HiveMind.Server.Services;

public class LibraryService: BaseService
{
    public LibraryService(sqliteDBContext context) : base(context) { }

    public IEnumerable<Library> GetAllLibraries()
    {
        return _context.Libraries;
    }

    public IEnumerable<Library> GetUnprocessedLibraries()
    {
        return _context.Libraries.Where(l => l.IsProcessed == false);
    }

    public void AddLibrary(Library library)
    {
        _context.Libraries.Add(library);
        _context.SaveChanges();
    }

    public Library? GetLibraryByID(int id)
    {
        return _context.Libraries.Find(id);
    }

    public void Update(Library library)
    {
        _context.Libraries.Update(library);
        _context.SaveChanges();
    }

    public void MarkLibraryAsProcessed(Library library)
    {
        library.IsProcessed = true;
        _context.Libraries.Update(library);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var library = _context.Libraries.Find(id);
        if (library != null)
        {
            _context.Libraries.Remove(library);
            _context.SaveChanges();
        }
    }
}
