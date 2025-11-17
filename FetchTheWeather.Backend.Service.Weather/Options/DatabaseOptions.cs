namespace FetchTheWeather.Backend.Service.Weather.Options;

public class DatabaseOptions
{
    public string Host { get; init; } = null!;
    public int Port { get; init; } = 3306;
    
    public string Database { get; init; } = null!;
    public string User { get; init; } = null!;
    public string Password { get; init; } = null!;
}