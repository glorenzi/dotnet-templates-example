using Moq;
using System;
using WebApiTemplate.Controllers.WeatherForecast;
using Xunit;

namespace WebApiTemplate.Tests.Unit;

public class WeatherForecastControllerTests
{
    [Fact]
    public void Get_ReturnsExpectedValue()
    {
        const int daysAhead = 10;
        WeatherForecast expectedResult = new(DateTime.Now, 25, "Sunny");

        var mockService = new Mock<IWeatherForecastService>();
        mockService
            .Setup(s => s.GetRandomForecast(daysAhead))
            .Returns(expectedResult);

        WeatherForecastController controller = new(mockService.Object);
        WeatherForecast result = controller.Get(daysAhead);

        Assert.NotNull(result);
        Assert.Equal(expectedResult, result);
        mockService.Verify(a => a.GetRandomForecast(daysAhead), Times.Once);
    }

    [Fact]
    public void Get_ThrowsException_IfInvalidParameters()
    {
        const int daysAhead = -5;

        WeatherForecastController controller = new(Mock.Of<IWeatherForecastService>());

        var exception = Assert.Throws<InvalidParametersException>(() => controller.Get(daysAhead));

        Assert.Equal($"Invalid value of days ahead: {daysAhead}", exception.Message);
    }
}
