using CleanArchitecture.Core.Dtos;

namespace CleanArchitecture.Core.Interfaces;

public interface IWeatherForecastService
{
    public WeatherForecastDto GetWeatherForecastAsync();
}