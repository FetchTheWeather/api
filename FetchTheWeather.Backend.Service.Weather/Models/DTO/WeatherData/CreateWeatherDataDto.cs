namespace FetchTheWeather.Backend.Service.Weather.Models.DTO.WeatherData;

public class CreateWeatherDataDto
{
    public Guid WeatherStationId { get; init; }

    public float TemperatureCelsius { get; init; }
    public float AirPressureHpa { get; init; }
    public float AirQualityPpm { get; init; }
    public float HumidityPercent { get; init; }

    public DateTime Timestamp { get; init; }
}