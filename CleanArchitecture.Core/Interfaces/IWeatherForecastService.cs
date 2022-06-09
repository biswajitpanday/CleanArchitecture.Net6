using CleanArchitecture.Core.Dtos;

namespace CleanArchitecture.Core.Interfaces;

public interface IWeatherForecastService
{
    Task<List<WeatherForecastDto>> Get();
    Task<WeatherForecastDto> Get(Guid id);
    Task<WeatherForecastDto> Create();
    Task<WeatherForecastDto> Update(WeatherForecastDto model);
    Task<bool> Delete(Guid id);
}