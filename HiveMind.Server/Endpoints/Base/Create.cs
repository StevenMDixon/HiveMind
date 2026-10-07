using FluentValidation;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Base;

public class Create
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", Handle)
            .WithRequestValidation<CreateRequest>()
            .WithName("Create");
    }

    public class Validator : AbstractValidator<CreateRequest>
    {
        public Validator()
        {

        }
    }

    public record CreateRequest();

    public static Results<Ok, NoContent, ValidationProblem> Handle(IService service, [FromBody] CreateRequest createRequest)
    {
        throw new NotImplementedException();
    }
}
