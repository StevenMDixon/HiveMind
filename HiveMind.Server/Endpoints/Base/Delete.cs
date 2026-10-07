using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Base;

public class Delete
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id:int}", Handle)
            .WithName("Delete");
    }

    public static Results<Ok, NoContent, ValidationProblem> Handle(IService service, [FromRoute] int id)
    {
        throw new NotImplementedException();
    }
}
