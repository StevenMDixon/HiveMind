using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Libraries;

public class Get
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:int}", Handle).WithName("GetLibraryById");
    }

    public record Library(int Id, string Name, string Path, string PathsToIgnore, LibraryType Type);

    public static Results<Ok<Library>, NotFound<string>> Handle(LibraryService libraryService, [FromRoute] int id)
    {
        var library = libraryService.GetLibraryByID(id);
        if (library is not null)
        {
            return TypedResults.Ok(new Library(library.Id, library.Name, library.Path, library.PathsToIgnore, library.Type));
        }

        return TypedResults.NotFound($"A library with the ID: {id} was not found.");
    }
}
