using FluentValidation;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Settings;

public class UpdateSettings
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/{id:int}", Handle)
            //.WithRequestValidation<SettingRequest>()
            .WithName("UpdateSetting");
            //.ProducesValidationProblem();
    }


    public record SettingRequest(string value);

    public static Results<Ok, NotFound<string>, ValidationProblem> Handle(SettingsService settingService, [FromRoute] int id, [FromBody] SettingRequest request)
    {
        var setting = settingService.GetById(id);

        if (setting is not null)
        {
            setting.Value = request.value;
            settingService.UpdateSetting(setting);
            return TypedResults.Ok();
        }

        return TypedResults.NotFound($"A query with the ID: {id} was not found.");
    }
}
