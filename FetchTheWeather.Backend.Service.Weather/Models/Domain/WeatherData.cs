namespace FetchTheWeather.Backend.Service.Weather.Models.Domain;

public class WeatherData
{
    public Guid Id { get; set; }
    public Guid WeatherStationId { get; set; }

    public float TemperatureCelsius { get; set; }
    public float AirPressureHpa { get; set; }
    public float AirQualityPpm { get; set; }
    public float HumidityPercent { get; set; }

    public DateTime Timestamp { get; set; }
}