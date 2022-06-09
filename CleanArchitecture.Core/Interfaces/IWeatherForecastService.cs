using CleanArchitecture.Core.Dtos;

namespace CleanArchitecture.Core.Interfaces;

public interface IWeatherForecastService
{
    Task<List<WeatherForecastDto>> GetWeatherForecastAsync();
    Task<WeatherForecastDto> StoreWeatherForecastAsync();
    Task<WeatherForecastDto> Update(WeatherForecastDto model);
    Task<bool> Delete(Guid id);
}