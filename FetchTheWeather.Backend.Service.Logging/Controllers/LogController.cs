using FetchTheWeather.Backend.Service.Logging.Mappers;
using FetchTheWeather.Backend.Service.Logging.Models.DTO.LogEntry;
using FetchTheWeather.Backend.Service.Logging.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FetchTheWeather.Backend.Service.Logging.Controllers;

[ApiController, Route("ls/logging/logs")]
public class LogController(ILogRepository repository) : ControllerBase
{
    [HttpGet("{logId:guid}")]
    public async Task<IActionResult> GetLogEntryAsync([FromRoute] Guid logId)
    {
        var log = await repository.GetLogEntryAsync(logId);
        if (log is null) return NotFound();

        return Ok(log.ToGetDto());
    }

    [HttpPost]
    public async Task<IActionResult> AddLogEntryAsync([FromBody] CreateLogEntryDto logEntry)
    {
        var log = await repository.AddLogEntryAsync(logEntry);
        return Ok(log.ToGetDto());
    }
}