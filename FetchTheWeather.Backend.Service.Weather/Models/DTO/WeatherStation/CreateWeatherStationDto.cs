namespace FetchTheWeather.Backend.Service.Weather.Models.DTO.WeatherStation;

public class CreateWeatherStationDto
{
    public string Name { get; init; } = null!;
    public string Location { get; init; } = null!;
}