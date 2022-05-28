using Serilog;

namespace CleanArchitecture.Api.Helpers;

public static class Extension
{
    public static void RegisterSeriLog(this IServiceCollection services)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("serilogconfig.json")
            .Build();
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(config)
            .CreateLogger();
    }
}