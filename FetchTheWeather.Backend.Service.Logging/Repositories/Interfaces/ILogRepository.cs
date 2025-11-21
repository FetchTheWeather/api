using FetchTheWeather.Backend.Service.Logging.Models.Domain;
using FetchTheWeather.Backend.Service.Logging.Models.DTO.LogEntry;

namespace FetchTheWeather.Backend.Service.Logging.Repositories.Interfaces;

public interface ILogRepository
{
    Task<LogEntry> AddLogEntryAsync(CreateLogEntryDto entry);
    Task<LogEntry?> GetLogEntryAsync(Guid logEntry);
}