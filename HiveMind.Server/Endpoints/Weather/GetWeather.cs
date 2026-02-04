using Microsoft.AspNetCore.Http.HttpResults;
using static HiveMind.Server.Endpoints.Weather.GetWeather;

namespace HiveMind.Server.Endpoints.Weather;

public static class GetWeather
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", Handle).WithName("GetWeatherForecast");
    }

    public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }

    public static string[] summaries { get; } = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };


    public static Results<Ok<IEnumerable<WeatherForecast>>, NotFound> Handle()
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();

        return TypedResults.Ok<IEnumerable<WeatherForecast>>(forecast);
    }
}
