using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace WebApiTemplate.Tests;

public class ApiTestWebApplicationFactory : WebApplicationFactory<Startup>
{
    private readonly string[] _args;
    private Action<IServiceCollection>? _configServices;

    public ApiTestWebApplicationFactory(string[]? args = null)
    {
        _args = args ?? Array.Empty<string>();
    }

    public void SetConfigureServicesCallback(Action<IServiceCollection> configServices)
    {
        _configServices = sc =>
        {
            configServices.Invoke(sc);
        };
    }

    protected override IHostBuilder CreateHostBuilder()
        => new ApiStartup(_args).CreateHostBuilder(_configServices);
}
