using HiveMind.Server.Entities;

namespace HiveMind.Server.Services;

public class LibraryService(SqliteDBContext context) : BaseService<Library>(context), ILibraryService
{
    public IEnumerable<Library> GetUnprocessedLibraries()
    {
        return Get().Where(l => l.IsProcessed == false);
    }

    public Library? GetById(int Id)
    {
        return Get().FirstOrDefault(x => x.Id == Id);
    }

    public void MarkLibraryAsProcessed(Library library)
    {
        library.IsProcessed = true;
        _context.Libraries.Update(library);
        _context.SaveChanges();
    }
}
