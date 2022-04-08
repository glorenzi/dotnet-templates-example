using Microsoft.Extensions.DependencyInjection.Extensions;
using WebApiTemplate.Controllers.WeatherForecast;

namespace WebApiTemplate;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration) => _configuration = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddHttpClient();

        AddWebApiServices(services);

        services.AddControllers(opt =>
        {
            opt.Filters.Add<ExceptionFilter>();
        });
        services.AddAuthorization();
        services.AddHealthChecks();
        services.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHealthChecks("/health");
        });
    }

    private static void AddWebApiServices(IServiceCollection services)
    {
        // TODO: Register your services here
        services.TryAddTransient<IWeatherForecastService, WeatherForecastService>();
    }
}
