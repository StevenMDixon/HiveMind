using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Lineup;

public static class Get
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:int}", Handle).WithName("GetLineupById");
    }

    public record Lineup(int Id, string Name, TimeOnly StartTime, string JsonData);

    public static Results<Ok<Lineup>, NotFound<string>> Handle(LineupService lineupService, [FromRoute] int id)
    {
        var lineup = lineupService.GetLineupByID(id);

        if (lineup is not null)
        {
            return TypedResults.Ok(new Lineup(
                lineup.Id, 
                lineup.Name, 
                lineup.StartTime, 
                lineup.JsonData
            ));
        }

        return TypedResults.NotFound($"A lineup with the ID: {id} was not found.");
    }
}
