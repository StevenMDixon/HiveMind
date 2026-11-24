using HiveMind.Server.Entities;

namespace HiveMind.Server.Services;

public interface ILibraryService: IBaseService<Library>
{
    public IEnumerable<Library> GetUnprocessedLibraries();
    public Library? GetById(int Id);
    public void MarkLibraryAsProcessed(Library library);
}