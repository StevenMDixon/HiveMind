using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HiveMind.Server.Endpoints.Libraries;

public class GetAll
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", Handle).WithName("GetAllLibraries");
    }

    public record Library(int Id, string Name, string Path, string PathsToIgnore, string Type, bool IsProcessed);
    public record GetAllLibrariesResponse(List<Library> Libraries);

    public record Parameters(
       string? Search,
       int? Page,
       int? Count
   );

    public static Results<Ok<GetAllLibrariesResponse>, NotFound> Handle(LibraryService libraryService, [AsParameters] Parameters filters)
    {
        var libraries = libraryService.GetAllLibraries();

        var pageSize = filters.Count ?? 100;
        var currentPage = filters.Page ?? 1;
        var dataCount = libraries.Count();

        if (filters.Search != null && filters.Search != string.Empty) libraries = libraries.Where(x => x.Name.ToLower().Contains(filters.Search.ToLower()));

        var m = (pageSize * currentPage);
        libraries = libraries.Skip((pageSize * (currentPage - 1))).Take(pageSize);

        return TypedResults.Ok(new GetAllLibrariesResponse(libraries.Select(x => new Library(x.Id, x.Name, x.Path, x.PathsToIgnore, x.Type.ToString(), x.IsProcessed)).ToList()));
    }
}
