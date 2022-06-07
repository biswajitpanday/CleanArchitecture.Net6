using AutoMapper;
using CleanArchitecture.Core.Dtos;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Interfaces.Repositories;

namespace CleanArchitecture.Service;

public class WeatherForecastService : IWeatherForecastService
{
    private readonly IMapper _mapper;
    private readonly IWeatherForecastRepository _weatherForecastRepository;

    public WeatherForecastService(IMapper mapper,
        IWeatherForecastRepository weatherForecastRepository)
    {
        _mapper = mapper;
        _weatherForecastRepository = weatherForecastRepository;
    }

    public async Task<WeatherForecastDto> StoreWeatherForecastAsync()
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
            Console.WriteLine(e);
            throw;
        }
        
        return _mapper.Map<WeatherForecastDto>(weatherForecastData);
    }

    public async Task<List<WeatherForecastDto>> GetWeatherForecastAsync()
    {
        var response = await _weatherForecastRepository.ListAsync();
        return _mapper.Map<List<WeatherForecastDto>>(response);
    }
}