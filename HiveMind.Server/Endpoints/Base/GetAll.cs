using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace HiveMind.Server.Endpoints.Base;

public class GetAll
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", Handle)
            .WithName("GetAll");
    }

    public static Results<Ok, NoContent, ValidationProblem> Handle(IService service)
    {
        throw new NotImplementedException();
    }
}
