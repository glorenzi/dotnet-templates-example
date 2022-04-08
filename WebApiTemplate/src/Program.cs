namespace WebApiTemplate;

public static class Program
{
    public static void Main(string[] args)
    {
        new ApiStartup(args).Run();
    }
}

public class ApiStartup
{
    private readonly string[] _args;

    public ApiStartup(string[] args) => _args = args;

    public void Run()
    {
        CreateHostBuilder().Build().Run();
    }

    public IHostBuilder CreateHostBuilder(Action<IServiceCollection>? configServices = null) =>
        Host.CreateDefaultBuilder(_args)
            .ConfigureAppConfiguration(configHost => configHost.AddEnvironmentVariables())
            .ConfigureServices(configServices ?? (_ => { }))
            .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
            .UseMusementLogging();
}
