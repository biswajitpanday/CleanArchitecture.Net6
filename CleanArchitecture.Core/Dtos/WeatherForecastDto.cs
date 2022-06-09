using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces.Common;

namespace CleanArchitecture.Core.Dtos;

public class WeatherForecastDto : IMapFrom<WeatherForecastEntity>
{
    public string? Id { get; set; }
    public string? WeatherCondition { get; set; }
    public long TemperatureInFh { get; set; }
    public long TemperatureInCelsius { get; set; }
    public long RainProbability { get; set; }
    public string? WindDirection { get; set; }
    public long WindSpeed { get; set; }
    public long Humidity { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdate { get; set; }
    public bool IsDeleted { get; set; }
}