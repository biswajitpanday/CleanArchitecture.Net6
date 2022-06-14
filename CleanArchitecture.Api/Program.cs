using CleanArchitecture.Api.Helpers;
using CleanArchitecture.Api.Middleware;
using CleanArchitecture.Core.Converters;
using CleanArchitecture.Core.Settings;
using DotNetCore.Logging;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => options.ModelBinderProviders.Insert(0, new CustomModelBinderProvider()))
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new TrimStringConverter());
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.RegisterSwagger();
builder.Host.UseSerilog();
Extension.RegisterSeriLog();
builder.Services.AddCorsPolicy();
builder.Services.AddDbContext();
builder.Services.RegisterIdentity();
builder.RegisterAuthentication();
builder.Services.AddServices();
builder.Services.AddRepositories();
builder.Services.RegisterAutoMapper();
builder.Services.AddResponseCompression();

builder.Configuration.GetSection("AppSettings").Get<AppSettings>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AppPolicy");
app.UseHttpsRedirection();
app.UseRouting();
app.UseResponseCompression();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();

app.Run();
