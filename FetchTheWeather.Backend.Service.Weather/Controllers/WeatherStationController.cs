using FetchTheWeather.Backend.Service.Weather.Models.Domain;
using FetchTheWeather.Backend.Service.Weather.Models.DTO.WeatherStation;
using FetchTheWeather.Backend.Service.Weather.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FetchTheWeather.Backend.Service.Weather.Controllers;

[ApiController, Route("weather/station")]
public class WeatherStationController(IWeatherStationRepository repository) : ControllerBase
{
    // TODO - Add the following:
    // - Validation
    // - Logging
    // - Error handling

    [HttpPost]
    public async Task<IActionResult> CreateWeatherStation([FromBody] CreateWeatherStationDto dto)
    {
        var createdStation = await repository.CreateWeatherStationAsync(dto);
        return CreatedAtAction(nameof(GetWeatherStationById), new { id = createdStation.Id }, ToGetDto(createdStation));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllWeatherStations()
    {
        var data = await repository.GetAllWeatherStationsAsync();
        var result = data.Select(ToGetDto).ToList();

        return Ok(result);
    }

    [HttpGet, Route("{id:guid}")]
    public async Task<IActionResult> GetWeatherStationById([FromRoute] Guid id)
    {
        var station = await repository.GetWeatherStationByIdAsync(id);
        return station is null ? NotFound() : Ok(ToGetDto(station));
    }

    // We convert the Domain Model to a DTO to avoid exposing internal details
    // TODO - Move to Extension method
    private GetWeatherStationDto ToGetDto(WeatherStation createdStation) => new()
    {
        Id = createdStation.Id,
        Name = createdStation.Name,
        Location = createdStation.Location
    };
}