using CleanArchitecture.Core.Dtos;
using CleanArchitecture.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CleanArchitecture.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IWeatherForecastService _weatherForecastService;

    public WeatherForecastController(ILogger<WeatherForecastController> logger,
        IWeatherForecastService weatherForecastService)
    {
        _logger = logger;
        _weatherForecastService = weatherForecastService;
    }

    [HttpGet]
    public async Task<ActionResult<WeatherForecastDto>> Get()
    {
        _logger.LogInformation("Collecting WeatherForecast List...");
        var data = await _weatherForecastService.GetWeatherForecastAsync();
        _logger.LogInformation($"Success Collecting WeatherForecast List. Data: {JsonConvert.SerializeObject(data)}");
        return Ok(data);
    }

    [HttpPost]
    public async Task<ActionResult<List<WeatherForecastDto>>> Create()
    {
        _logger.LogInformation("Creating WeatherForecast...");
        var data = await _weatherForecastService.StoreWeatherForecastAsync();
        _logger.LogInformation("Success Creating WeatherForecast...");
        return Ok(data);
    }

    [HttpPut]
    public async Task<ActionResult<List<WeatherForecastDto>>> Update(WeatherForecastDto model)
    {
        _logger.LogInformation("Updating WeatherForecast...");
        var data = await _weatherForecastService.Update(model);
        _logger.LogInformation("Success Updating WeatherForecast...");
        return Ok(data);
    }
}