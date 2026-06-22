using FluentValidation;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Stations;

public class UpdateStation
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("/{id:int}", Handle)
            .WithRequestValidation<StationRequest>()
            .WithName("UpdateStation")
            .ProducesValidationProblem();
    }

    public class Validator: AbstractValidator<StationRequest>
    {
        public Validator()
        {
            RuleFor(x => x.StationName).NotEmpty().MaximumLength(100);
        }
    }

    public record StationRequest(string StationName, int StationNumber);
    public static Results<Ok, NotFound<string>, ValidationProblem> Handle(StationService stationService, [FromRoute] int id, [FromBody] StationRequest request)
    {
        var station = stationService.GetStationByID(id);

        if(station is not null)
        {
            station.StationName = request.StationName;
            station.StationNumber = request.StationNumber;
            stationService.Update(station);
            return TypedResults.Ok();
        }

        return TypedResults.NotFound($"A station with the ID: {id} was not found.");
    }
}
