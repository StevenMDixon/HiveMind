using FluentValidation;
using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Libraries;

public class CreateLibrary
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", Handle)
            .WithName("CreateLibrary");
    }

    public record LibraryRequest(string LibraryName, string LibraryPath, string PathsToIgnore, LibraryType LibraryType);

    public static Results<Ok, NoContent, ValidationProblem> Handle(LibraryService libraryService, [FromBody] LibraryRequest request)
    {
        var newLibrary = new Entities.Library
        {
            LibraryName = request.LibraryName,
            LibraryPath = request.LibraryPath,
            PathsToIgnore = request.PathsToIgnore,
            LibraryType = request.LibraryType

        };

        libraryService.AddLibrary(newLibrary);

        return TypedResults.NoContent();
    }
}
