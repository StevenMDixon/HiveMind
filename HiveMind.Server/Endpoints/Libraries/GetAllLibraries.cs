using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HiveMind.Server.Endpoints.Libraries;

public class GetAllLibraries
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", Handle).WithName("GetAllLibraries");
    }

    public record Library(int LibraryId, string LibraryName);
    public record GetAllLibrariesResponse(List<Library> Libraries);
    public static Results<Ok<GetAllLibrariesResponse>, NotFound> Handle(LibraryService libraryService)
    {
        var libraries = libraryService.GetAllLibraries();

        return TypedResults.Ok(new GetAllLibrariesResponse(libraries.Select(x => new Library(x.LibraryId, x.LibraryName)).ToList()));
    }
}
