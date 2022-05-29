using Microsoft.OpenApi.Models;
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

    public static void RegisterSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1",
                new OpenApiInfo
                    {Title = "CleanArchitecture.Net6", Version = "v1", Description = "Onion Architecture Using .Net6"});
        });
    }
}