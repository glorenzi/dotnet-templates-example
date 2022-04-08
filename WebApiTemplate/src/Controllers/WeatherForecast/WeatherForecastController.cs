using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApiTemplate.Controllers.WeatherForecast;

[ApiController]
[Route("api/weatherforecast")]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherForecastService _service;

    public WeatherForecastController(IWeatherForecastService service)
    {
        _service = service;
    }

    [HttpGet("get")]
    public WeatherForecast Get([Required][FromQuery] int daysAhead)
    {
        if (daysAhead < 0)
        { 
            throw new InvalidParametersException($"Invalid value of days ahead: {daysAhead}");
        }

        return _service.GetRandomForecast(daysAhead);
    }
}
