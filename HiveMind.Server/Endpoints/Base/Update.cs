using FluentValidation;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Base;

public class Update
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("/", Handle)
            .WithRequestValidation<UpdateRequest>()
            .WithName("Update");
    }

    public class Validator : AbstractValidator<UpdateRequest>
    {
        public Validator()
        {
            //RuleFor(x => x.LibraryName).NotEmpty();
        }
    }


    public record UpdateRequest();

    public static Results<Ok, NoContent, ValidationProblem> Handle(IService serviceType, [FromBody] UpdateRequest updateRequest)
    {
        throw new NotImplementedException();
    }
}
