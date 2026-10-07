using FluentValidation;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Settings;

public class Update
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/{id:int}", Handle)
            .WithName("UpdateSetting");
    }


    public record SettingRequest(string Value);

    public static Results<Ok, NotFound<string>, ValidationProblem> Handle(SettingsService settingService, [FromRoute] int id, [FromBody] SettingRequest request)
    {
        var setting = settingService.GetById(id);

        if (setting is not null)
        {
            setting.Value = request.Value;
            settingService.UpdateSetting(setting);
            return TypedResults.Ok();
        }

        return TypedResults.NotFound($"A query with the ID: {id} was not found.");
    }
}
