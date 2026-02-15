using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Libraries;

public class GetLibraryById
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:int}", Handle).WithName("GetLibraryById");
    }

    public record Library(int LibraryId, string LibraryName);

    public static Results<Ok<Library>, NotFound<string>> Handle(LibraryService libraryService, [FromRoute] int id)
    {
        var library = libraryService.GetLibraryByID(id);
        if (library is not null)
        {
            return TypedResults.Ok(new Library(library.LibraryId, library.LibraryName));
        }

        return TypedResults.NotFound($"A library with the ID: {id} was not found.");
    }
}
