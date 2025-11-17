using FetchTheWeather.Backend.Service.Logging.Models.Domain;
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
        return log is null ? NotFound() : Ok(log);
    }

    [HttpPost]
    public async Task<IActionResult> AddLogEntryAsync([FromBody] LogEntry logEntry)
    {
        var log = await repository.AddLogEntryAsync(logEntry);
        return Ok(log);
    }
}