namespace FetchTheWeather.Backend.Service.Logging.Models.DTO.LogEntry;

public class GetLogEntryDto
{
    public Guid Id { get; init; }

    public string EventType { get; init; } = null!;
    public string Description { get; init; } = null!;

    public DateTime TimeStamp { get; init; }
}