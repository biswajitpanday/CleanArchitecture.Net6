using CleanArchitecture.Api.Helpers;
using CleanArchitecture.Core.Converters;
using DotNetCore.Logging;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog();
builder.Services.RegisterSeriLog();

builder.Services.AddControllers(options => options.ModelBinderProviders.Insert(0, new CustomModelBinderProvider()))
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new TrimStringConverter());
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.RegisterSwagger();
builder.Services.AddCorsPolicy();
builder.Services.AddDbContext();
builder.Services.AddServices();
builder.Services.AddRepositories();
builder.Services.RegisterAutoMapper();



var app = builder.Build();

app.UseCors("AppPolicy");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
