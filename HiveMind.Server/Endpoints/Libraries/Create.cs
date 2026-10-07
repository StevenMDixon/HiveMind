using FluentValidation;
using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Libraries;

public class Create
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", Handle)
            .WithName("CreateLibrary");
    }

    public record LibraryRequest(string Name, string Path, string PathsToIgnore, LibraryType Type);

    public static Results<Ok, NoContent, ValidationProblem> Handle(LibraryService libraryService, [FromBody] LibraryRequest request)
    {
        var newLibrary = new Entities.Library
        {
            Name = request.Name,
            Path = request.Path,
            PathsToIgnore = request.PathsToIgnore,
            Type = request.Type
        };

        libraryService.AddLibrary(newLibrary);

        return TypedResults.NoContent();
    }
}
