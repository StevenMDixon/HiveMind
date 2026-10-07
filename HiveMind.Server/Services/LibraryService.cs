using HiveMind.Server.Entities;

namespace HiveMind.Server.Services;

public class LibraryService(SqliteDBContext context) : BaseService<Library>(context)
{
    public IEnumerable<Library> GetAllLibraries()
    {
        return Get();
    }

    public IEnumerable<Library> GetUnprocessedLibraries()
    {
        return Get().Where(l => l.IsProcessed == false);
    }

    public void AddLibrary(Library library)
    {
        Create(library);
        _context.SaveChanges();
    }

    public Library? GetLibraryByID(int Id)
    {
        return Get().FirstOrDefault(x => x.Id == Id);
    }


    public void MarkLibraryAsProcessed(Library library)
    {
        library.IsProcessed = true;
        _context.Libraries.Update(library);
        _context.SaveChanges();
    }

    public void Delete(int Id)
    {
        var library = Get().FirstOrDefault(x => x.Id == Id);
        if (library != null)
        {
            Delete(library);
            _context.SaveChanges();
        }
    }
}
