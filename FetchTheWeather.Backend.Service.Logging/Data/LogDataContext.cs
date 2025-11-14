using FetchTheWeather.Backend.Service.Logging.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace FetchTheWeather.Backend.Service.Logging.Data;

public class LogDataContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<LogEntry> LogEntries { get; init; } = null!;
}