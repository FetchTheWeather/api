using FetchTheWeather.Backend.Service.Logging.Models.Domain;
using FetchTheWeather.Backend.Service.Logging.Models.DTO.LogEntry;

namespace FetchTheWeather.Backend.Service.Logging.Mappers;

public static class LogEntryMapper
{
    public static LogEntry ToDomain(this CreateLogEntryDto source) => new()
    {
        EventType = source.EventType,
        Description = source.Description,

        TimeStamp = source.TimeStamp,
    };

    public static GetLogEntryDto ToGetDto(this LogEntry source) => new()
    {
        Id = source.Id,

        EventType = source.EventType,
        Description = source.Description,

        TimeStamp = source.TimeStamp,
    };
}