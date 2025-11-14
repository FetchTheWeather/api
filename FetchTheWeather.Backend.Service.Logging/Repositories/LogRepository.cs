using FetchTheWeather.Backend.Service.Logging.Data;
using FetchTheWeather.Backend.Service.Logging.Models.Domain;
using FetchTheWeather.Backend.Service.Logging.Repositories.Interfaces;

namespace FetchTheWeather.Backend.Service.Logging.Repositories;

public class LogRepository(LogDataContext Context): ILogRepository
{
    public async Task<LogEntry> AddLogEntryAsync(LogEntry logEntry)
    {
        Context.LogEntries.Add(logEntry);
        await Context.SaveChangesAsync();
        return logEntry;
    }

    public async Task<LogEntry?> GetLogEntryAsync(Guid logEntry)
    {
        return await Context.LogEntries.FindAsync(logEntry);
    }
}