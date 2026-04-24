using FluentValidation;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Libraries;

public class ReprocessLibrary
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("/reprocess/{id:int}", Handle)
            //.WithRequestValidation<LibraryRequest>()
            .WithName("ReprocessLibrary")
            .ProducesValidationProblem();
    }

    //public class Validator : AbstractValidator<LibraryRequest>
    //{
    //    public Validator()
    //    {
    //        RuleFor(x => x.LibraryName).NotEmpty();
    //        RuleFor(x => x.LibraryPath).NotEmpty();
    //        RuleFor(x => x.LibraryType).IsInEnum();
    //    }
    //}

    //public record LibraryRequest(string LibraryName, string LibraryPath, string PathsToIgnore, LibraryType LibraryType);

    public static Results<Ok, NotFound<string>, NoContent, ValidationProblem> Handle(LibraryService libraryService, [FromRoute] int id)
    {
        var library = libraryService.GetLibraryByID(id);

        if (library == null) return TypedResults.NotFound($"A query with the ID: {id} was not found.");

        library.IsProcessed = false;
        
        libraryService.Update(library);

        return TypedResults.NoContent();
    }
}
