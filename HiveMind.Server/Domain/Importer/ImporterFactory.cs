using HiveMind.Server.Domain.Enums;

namespace HiveMind.Server.Domain.Importer;

public static class ImporterFactory
{
    public static IImporter Resolve(LibraryType libraryType)
    {
        return libraryType switch
        {
            LibraryType.Show => new ShowImporter(),
            _ => new DefaultImporter()
        };
    }
}
