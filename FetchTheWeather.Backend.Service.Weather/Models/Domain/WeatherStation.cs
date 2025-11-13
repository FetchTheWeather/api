namespace FetchTheWeather.Backend.Service.Weather.Models.Domain;

public class WeatherStation
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;
    public string Location { get; set; } = null!;
}