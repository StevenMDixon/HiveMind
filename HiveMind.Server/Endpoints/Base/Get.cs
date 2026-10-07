using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace HiveMind.Server.Endpoints.Base;

public class Get
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:int}", Handle)
            .WithName("Get");
    }

    public static Results<Ok, NoContent, ValidationProblem> Handle(IService service, [FromRoute] int id)
    {
        throw new NotImplementedException();
    }
}
