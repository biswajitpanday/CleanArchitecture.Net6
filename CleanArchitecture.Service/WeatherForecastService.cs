using AutoMapper;
using CleanArchitecture.Core.Dtos;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;

namespace CleanArchitecture.Service;

public class WeatherForecastService : IWeatherForecastService
{
    private readonly IMapper _mapper;

    public WeatherForecastService(IMapper mapper)
    {
        _mapper = mapper;
    }

    public WeatherForecastDto GetWeatherForecastAsync()
    {
        var weatherForecastData = new WeatherForecastEntity
        {
            Id = Guid.NewGuid().ToString(),
            WeatherCondition = "Cloudy",
            TemperatureInFh = 32,
            TemperatureInCelsius = 89,
            RainProbability = 45,
            WindDirection = "South",
            WindSpeed = 19,
            Humidity = 67,
            CreatedAt = DateTime.UtcNow,
            LastUpdated = DateTime.UtcNow - TimeSpan.FromMinutes(5),
            IsDeleted = false
        };
        var response = _mapper.Map<WeatherForecastDto>(weatherForecastData);
        return response;
    }
}