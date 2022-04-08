using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using WebApiTemplate.Controllers.WeatherForecast;
using Xunit;

namespace WebApiTemplate.Tests.Integration;

public class WeatherForecastApiTests
{
    private readonly ApiTestWebApplicationFactory _waf = new();

    [Fact]
    public async Task Get_ReturnsExpectedResponse()
    {
        const int daysAhead = 5;
        WeatherForecast expectedResult = new(DateTime.Now.AddDays(daysAhead), 25, "Sunny");

        var mockService = new Mock<IWeatherForecastService>();
        mockService
            .Setup(s => s.GetRandomForecast(daysAhead))
            .Returns(expectedResult);

        _waf.SetConfigureServicesCallback(s => s.AddSingleton(mockService.Object));

        using HttpClient client = _waf.CreateClient();
        HttpResponseMessage response = await client.GetAsync(
            new Uri(QueryHelpers.AddQueryString("api/weatherforecast/get", "daysAhead", daysAhead.ToString(CultureInfo.InvariantCulture)), UriKind.Relative)
        );

        HttpContent content = response.Content;
        WeatherForecast? result = await JsonSerializer.DeserializeAsync<WeatherForecast>(
            await content.ReadAsStreamAsync(),
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
        );

        Assert.True(response.IsSuccessStatusCode);
        Assert.NotNull(result);
        Assert.Equal(expectedResult, result);
        mockService.Verify(a => a.GetRandomForecast(daysAhead), Times.Once);
    }

    [Fact]
    public async Task Get_ReturnsBadRequest_IfInvalidParameters()
    {
        const int daysAhead = -5;

        _waf.SetConfigureServicesCallback(s => s.AddSingleton(Mock.Of<IWeatherForecastService>()));

        using HttpClient client = _waf.CreateClient();
        HttpResponseMessage response = await client.GetAsync(
            new Uri(QueryHelpers.AddQueryString("api/weatherforecast/get", "daysAhead", daysAhead.ToString(CultureInfo.InvariantCulture)), UriKind.Relative)
        );

        HttpContent content = response.Content;

        Assert.False(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
