namespace FetchTheWeather.Backend.Service.Logging.Models.Domain;

public class LogEntry
{
    public Guid Id { get; set; }

    public string EventType { get; set; } = null!;
    public string Description { get; set; } = null!;

    public DateTime TimeStamp { get; set; }
}