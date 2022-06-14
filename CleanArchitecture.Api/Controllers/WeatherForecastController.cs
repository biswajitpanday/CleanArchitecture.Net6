using CleanArchitecture.Core.Constants;
using CleanArchitecture.Core.Dtos;
using CleanArchitecture.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<WeatherForecastDto>>> Get()
    {
        _logger.LogInformation("Collecting WeatherForecast List...");
        var data = await _weatherForecastService.Get();
        _logger.LogInformation($"Success Collecting WeatherForecast List. \nData: {JsonConvert.SerializeObject(data)}");
        return Ok(data);
    }

    [HttpGet("id")]
    public async Task<ActionResult<WeatherForecastDto>> Get(Guid id)
    {
        _logger.LogInformation($"Collecting WeatherForecast Data of Id {id}");
        var data = await _weatherForecastService.Get(id);
        _logger.LogInformation($"Success Collecting WeatherForecast Data. \nData: {JsonConvert.SerializeObject(data)}");
        return Ok(data);
    }

    [Authorize(Roles = UserRoles.Admin)]
    [HttpPost]
    public async Task<ActionResult<WeatherForecastDto>> Create()
    {
        _logger.LogInformation("Creating WeatherForecast...");
        var data = await _weatherForecastService.Create();
        _logger.LogInformation("Success Creating WeatherForecast...");
        return Ok(data);
    }

    [HttpPut]
    public async Task<ActionResult<WeatherForecastDto>> Update(WeatherForecastDto model)
    {
        _logger.LogInformation("Updating WeatherForecast...");
        var data = await _weatherForecastService.Update(model);
        _logger.LogInformation("Success Updating WeatherForecast...");
        return Ok(data);
    }

    [HttpDelete("id")]
    public async Task<ActionResult<bool>> Delete(Guid id)
    {
        _logger.LogInformation($"Deleting WeatherForecast of Id {id}");
        var data = await _weatherForecastService.Delete(id);
        _logger.LogInformation("Success Deleting WeatherForecast...");
        return Ok(data);
    }
}