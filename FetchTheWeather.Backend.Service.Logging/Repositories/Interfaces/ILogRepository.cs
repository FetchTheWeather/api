using FetchTheWeather.Backend.Service.Logging.Models.Domain;

namespace FetchTheWeather.Backend.Service.Logging.Repositories.Interfaces;

public interface ILogRepository
{
    Task <LogEntry> AddLogEntryAsync(LogEntry logEntry);
    Task <LogEntry?> GetLogEntryAsync(Guid logEntry);
}