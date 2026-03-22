using FluentValidation;
using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;


namespace HiveMind.Server.Endpoints.Libraries;
public class UpdateLibrary
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("/{id:int}", Handle)
            .WithRequestValidation<LibraryRequest>()
            .WithName("UpdateLibrary")
            .ProducesValidationProblem();
    }

    public class Validator : AbstractValidator<LibraryRequest>
    {
        public Validator()
        {
            RuleFor(x => x.LibraryName).NotEmpty();
            RuleFor(x => x.LibraryPath).NotEmpty();
            RuleFor(x => x.LibraryType).IsInEnum();
        }
    }

    public record LibraryRequest(string LibraryName, string LibraryPath, LibraryType LibraryType);

    public static Results<Ok, NotFound<string>, NoContent, ValidationProblem> Handle(LibraryService libraryService, [FromRoute] int id, [FromBody] LibraryRequest request)
    {
       var library = libraryService.GetLibraryByID(id);

        if(library == null) return TypedResults.NotFound($"A collection with the ID: {id} was not found.");

        if (library.LibraryPath != request.LibraryPath) library.IsProcessed = false;

        library.LibraryPath = request.LibraryPath;
        library.LibraryName = request.LibraryName;
        library.LibraryType = request.LibraryType;

        libraryService.Update(library);

        return TypedResults.NoContent();
    }
}




