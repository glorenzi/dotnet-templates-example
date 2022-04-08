namespace WebApiTemplate.Controllers.WeatherForecast;

public interface IWeatherForecastService
{
    public WeatherForecast GetRandomForecast(int daysAhead);

}
