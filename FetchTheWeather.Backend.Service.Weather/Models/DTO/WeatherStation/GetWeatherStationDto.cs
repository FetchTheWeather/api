namespace FetchTheWeather.Backend.Service.Weather.Models.DTO.WeatherStation;

public class GetWeatherStationDto
{
    public Guid Id { get; set; }

    public string Name { get; init; } = null!;
    public string Location { get; init; } = null!;
}