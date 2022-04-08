namespace WebApiTemplate.Controllers.WeatherForecast;

public class WeatherForecastService : IWeatherForecastService
{
    private readonly ILogger<WeatherForecastService> _logger;
    private static readonly string[] _summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public WeatherForecastService(ILogger<WeatherForecastService> logger)
    {
        _logger = logger;
    }

    public WeatherForecast GetRandomForecast(int daysAhead)
    {
        var forecast = new WeatherForecast(
            DateTime.Now.Date.AddDays(daysAhead),
            Random.Shared.Next(-20, 55),
            _summaries.ElementAt(Random.Shared.Next(_summaries.Count()))
        );

        _logger.LogInformation($"Requested forecast for the day {forecast.Date.ToString("dd/MM/yyyy")}");

        return forecast;
    }
}
