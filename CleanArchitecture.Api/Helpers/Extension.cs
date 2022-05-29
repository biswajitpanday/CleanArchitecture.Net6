﻿using CleanArchitecture.Repository.DatabaseContext;
using DotNetCore.IoC;
using Microsoft.EntityFrameworkCore;
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

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    public static void RegisterSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1",
                new OpenApiInfo
                { Title = "CleanArchitecture.Net6", Version = "v1", Description = "Onion Architecture Using .Net6" });
        });
    }

    public static void AddCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(option =>
            option.AddPolicy("AppPolicy", builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); }));
    }

    public static void AddDbContext(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(services.GetConnectionString("DefaultConnection"));
        });
    }
}