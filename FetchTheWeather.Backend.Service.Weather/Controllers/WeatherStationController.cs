using FetchTheWeather.Backend.Service.Weather.Mappers;
using FetchTheWeather.Backend.Service.Weather.Models.Domain;
using FetchTheWeather.Backend.Service.Weather.Models.DTO.WeatherStation;
using FetchTheWeather.Backend.Service.Weather.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FetchTheWeather.Backend.Service.Weather.Controllers;

[ApiController, Route("ws/weather/station")]
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
        return CreatedAtAction(nameof(GetWeatherStationById), new { id = createdStation.Id }, createdStation.ToGetDto());
    }

    [HttpGet]
    public async Task<IActionResult> GetAllWeatherStations()
    {
        var data = await repository.GetAllWeatherStationsAsync();
        var result = data.Select(weatherStation => weatherStation.ToGetDto());

        return Ok(result);
    }

    [HttpGet, Route("{id:guid}")]
    public async Task<IActionResult> GetWeatherStationById([FromRoute] Guid id)
    {
        var station = await repository.GetWeatherStationByIdAsync(id);
        return station is null ? NotFound() : Ok(station.ToGetDto());
    }
}