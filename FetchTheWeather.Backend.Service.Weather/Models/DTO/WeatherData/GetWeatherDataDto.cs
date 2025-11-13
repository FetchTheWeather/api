namespace FetchTheWeather.Backend.Service.Weather.Models.DTO.WeatherData;

public class GetWeatherDataDto
{
    public Guid Id { get; set; }
    public Guid WeatherStationId { get; init; }

    public float TemperatureCelsius { get; init; }
    public float AirPressureHpa { get; init; }
    public float HumidityPercent { get; init; }
    public float WindSpeedKph { get; init; }

    public bool IsRaining { get; init; }
    public float RainfallMm { get; init; }

    public DateTime Timestamp { get; init; }
}