namespace CleanArchitecture.Core.Entities;

public class WeatherForecastEntity : BaseEntity
{
    public string? WeatherCondition { get; set; }
    public long TemperatureInFh { get; set; }
    public long TemperatureInCelsius { get; set; }
    public long RainProbability { get; set; }
    public string? WindDirection { get; set; }
    public long WindSpeed { get; set; }
    public long Humidity { get; set; }
}