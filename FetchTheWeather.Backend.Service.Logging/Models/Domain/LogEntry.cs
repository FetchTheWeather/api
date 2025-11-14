namespace FetchTheWeather.Backend.Service.Logging.Models.Domain;

public class LogEntry
{
    public Guid Id { get; set; }
    public string EventType { get; set; }
    public string Description { get; set; }
    public DateTime TimeStamp { get; set; }
}

