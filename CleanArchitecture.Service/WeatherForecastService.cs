using AutoMapper;
using CleanArchitecture.Core.Dtos;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Service;

public class WeatherForecastService : IWeatherForecastService
{
    private readonly ILogger<WeatherForecastService> _logger;
    private readonly IMapper _mapper;
    private readonly IWeatherForecastRepository _weatherForecastRepository;

    public WeatherForecastService(ILogger<WeatherForecastService> logger, IMapper mapper,
        IWeatherForecastRepository weatherForecastRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _weatherForecastRepository = weatherForecastRepository;
    }

    public async Task<List<WeatherForecastDto>> Get()
    {
        var response = await _weatherForecastRepository.ListAsync();
        return _mapper.Map<List<WeatherForecastDto>>(response);
    }

    public async Task<WeatherForecastDto> Get(Guid id)
    {
        var response = await _weatherForecastRepository.GetAsync(id);
        return _mapper.Map<WeatherForecastDto>(response);
    }

    public async Task<WeatherForecastDto> Create()
    {
        var weatherForecastData = new WeatherForecastEntity
        {
            WeatherCondition = "Cloudy",
            TemperatureInFh = 32,
            TemperatureInCelsius = 89,
            RainProbability = 45,
            WindDirection = "South",
            WindSpeed = 19,
            Humidity = 67,
            CreatedAt = DateTime.UtcNow,
            LastUpdate = DateTime.UtcNow - TimeSpan.FromMinutes(5),
            IsDeleted = false
        };
        try
        {
            await _weatherForecastRepository.AddAsync(weatherForecastData);
            await _weatherForecastRepository.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError($"Error Storing Data. Error: {e.Message} \n StackTrace: {e.StackTrace}");
        }
        return _mapper.Map<WeatherForecastDto>(weatherForecastData);
    }

    public async Task<WeatherForecastDto> Update(WeatherForecastDto model)
    {
        var toUpdate = await _weatherForecastRepository.GetAsync(Guid.Parse(model.Id));
        if (toUpdate == null)
            throw new Exception($"Weather forecast with Id {model.Id} not found.");
        toUpdate = _mapper.Map<WeatherForecastEntity>(model);
        await _weatherForecastRepository.UpdateAsync(toUpdate);
        await _weatherForecastRepository.SaveChangesAsync();
        return _mapper.Map<WeatherForecastDto>(toUpdate);
    }

    public async Task<bool> Delete(Guid id)
    {
        await _weatherForecastRepository.SoftDeleteAsync(id);
        await _weatherForecastRepository.SaveChangesAsync();
        return true;
    }
}