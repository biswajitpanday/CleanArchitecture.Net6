using CleanArchitecture.Core.Dtos;

namespace CleanArchitecture.Core.Interfaces;

public interface IWeatherForecastService
{
    Task<List<WeatherForecastDto>> GetWeatherForecastAsync();
    Task<WeatherForecastDto> StoreWeatherForecastAsync();
}