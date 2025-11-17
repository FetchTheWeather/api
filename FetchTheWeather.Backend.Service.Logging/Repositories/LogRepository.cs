using FetchTheWeather.Backend.Service.Logging.Data;
using FetchTheWeather.Backend.Service.Logging.Models.Domain;
using FetchTheWeather.Backend.Service.Logging.Repositories.Interfaces;

namespace FetchTheWeather.Backend.Service.Logging.Repositories;

public class LogRepository(LogDataContext context) : ILogRepository
{
    // TODO - Use DTOs instead of Domain Models
    public async Task<LogEntry> AddLogEntryAsync(LogEntry logEntry)
    {
        context.LogEntries.Add(logEntry);
        await context.SaveChangesAsync();

        return logEntry;
    }

    public async Task<LogEntry?> GetLogEntryAsync(Guid logEntry)
    {
        return await context.LogEntries.FindAsync(logEntry);
    }
}