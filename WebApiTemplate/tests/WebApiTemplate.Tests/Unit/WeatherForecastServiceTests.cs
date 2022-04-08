using Microsoft.Extensions.Logging;
using Moq;
using System;
using WebApiTemplate.Controllers.WeatherForecast;
using Xunit;

namespace WebApiTemplate.Tests.Unit;

public class WeatherForecastServiceTests
{
    private readonly Mock<ILogger<WeatherForecastService>> _mockLogger;

    public WeatherForecastServiceTests()
    {
        _mockLogger = new Mock<ILogger<WeatherForecastService>>();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(50)]
    public void GetRandomForecast_ShouldReturnsForecastForRequestedDayAndLog(int daysAhead)
    {
        DateTime expectedDate = DateTime.Now.AddDays(daysAhead).Date;

        WeatherForecastService service = new(_mockLogger.Object);
        WeatherForecast result = service.GetRandomForecast(daysAhead);

        Assert.Equal(expectedDate, result.Date);
        VerifyLogCallOnce(LogLevel.Information, $"Requested forecast for the day {expectedDate.Date.ToString("dd/MM/yyyy")}");
    }

    private void VerifyLogCallOnce(LogLevel expectedLogLevel, string expectedMessage)
    {
        _mockLogger.Verify(
            x => x.Log(
                expectedLogLevel,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((o, t) => string.Equals(expectedMessage, o.ToString(), StringComparison.OrdinalIgnoreCase)),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }
}
