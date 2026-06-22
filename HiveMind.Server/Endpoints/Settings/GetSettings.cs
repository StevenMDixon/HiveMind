using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HiveMind.Server.Endpoints.Settings;

public class GetSettings
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", Handle).WithName("GetAllSettings");
    }

    public record Setting(int SettingId, string Name, string Value);

    public record SettingsResponse(ICollection<Setting> Settings);

    public static Results<Ok<SettingsResponse>, NotFound> Handle(SettingsService settingsService)
    {
        var settings = settingsService.GetAllSettings();

        return TypedResults.Ok(new SettingsResponse(settings.Select(setting => new Setting(setting.SettingsId, setting.Name, setting.Value)).ToList()));
    }
}
