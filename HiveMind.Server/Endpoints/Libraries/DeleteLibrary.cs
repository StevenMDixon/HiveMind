using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Libraries;

public class DeleteLibrary
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id:int}", Handle);
    }
    public static async Task<Results<Ok, NotFound>> Handle(LibraryService libraryService, [FromRoute] int id)
    {
        var library = libraryService.GetLibraryByID(id);
        if (library is null)
        {
            return TypedResults.NotFound();
        }
        
        libraryService.Delete(id);

        return TypedResults.Ok();
    }
}
