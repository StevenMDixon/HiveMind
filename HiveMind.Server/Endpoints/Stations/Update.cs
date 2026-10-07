using FluentValidation;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Stations;

public class Update
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
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        }
    }

    public record StationRequest(string? Name, int? Number, string? Logo, int? StrategyId, int? DroneId);
    public static Results<Ok, NotFound<string>, ValidationProblem> Handle(StationService stationService, [FromRoute] int id, [FromBody] StationRequest request)
    {
        var station = stationService.GetStationByID(id);

        if(station is not null)
        {
            station.Name = request.Name ?? station.Name;
            station.Number = request.Number ?? station.Number;
            station.Logo = request.Logo ?? station.Logo;
            station.StrategyId = request.StrategyId ?? station.StrategyId;
            station.DroneId = request.DroneId ?? station.DroneId;
            stationService.Update(station);
            return TypedResults.Ok();
        }

        return TypedResults.NotFound($"A station with the ID: {id} was not found.");
    }
}
