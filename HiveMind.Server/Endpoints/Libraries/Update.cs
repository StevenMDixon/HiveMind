using FluentValidation;
using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;


namespace HiveMind.Server.Endpoints.Libraries;
public class Update
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
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Path).NotEmpty();
            RuleFor(x => x.Type).IsInEnum();
        }
    }

    public record LibraryRequest(string Name, string Path, string PathsToIgnore, LibraryType Type);

    public static Results<Ok, NotFound<string>, NoContent, ValidationProblem> Handle(LibraryService libraryService, [FromRoute] int id, [FromBody] LibraryRequest request)
    {
       var library = libraryService.GetLibraryByID(id);

        if(library == null) return TypedResults.NotFound($"A query with the ID: {id} was not found.");

        if (library.Path != request.Path || library.PathsToIgnore != request.PathsToIgnore) library.IsProcessed = false;

        library.Path = request.Path;
        library.Name = request.Name;
        library.Type = request.Type;
        library.PathsToIgnore = request.PathsToIgnore;

        libraryService.Update(library);

        return TypedResults.NoContent();
    }
}




