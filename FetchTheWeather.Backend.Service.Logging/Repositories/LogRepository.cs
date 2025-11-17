using FetchTheWeather.Backend.Service.Logging.Data;
using FetchTheWeather.Backend.Service.Logging.Models.Domain;
using FetchTheWeather.Backend.Service.Logging.Models.DTO.LogEntry;
using FetchTheWeather.Backend.Service.Logging.Repositories.Interfaces;

namespace FetchTheWeather.Backend.Service.Logging.Repositories;

public class LogRepository(LogDataContext context) : ILogRepository
{
    public async Task<LogEntry> AddLogEntryAsync(CreateLogEntryDto dot)
    {
        var logEntry = new LogEntry()
        {
            EventType = dot.EventType,
            Description = dot.Description,

            TimeStamp = DateTime.UtcNow
        };

        context.LogEntries.Add(logEntry);
        await context.SaveChangesAsync();

        return logEntry;
    }

    public async Task<LogEntry?> GetLogEntryAsync(Guid logEntry)
    {
        return await context.LogEntries.FindAsync(logEntry);
    }
}