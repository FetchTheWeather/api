using FetchTheWeather.Backend.Service.Weather.Models.Domain;
using FetchTheWeather.Backend.Service.Weather.Models.DTO.WeatherData;
using FetchTheWeather.Backend.Service.Weather.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FetchTheWeather.Backend.Service.Weather.Controllers;

[ApiController, Route("ws/weather/data")]
public class WeatherDataController(IWeatherDataRepository repository) : ControllerBase
{
    // TODO - Add the following:
    // - Validation
    // - Logging
    // - Error handling

    [HttpPost]
    public async Task<IActionResult> CreateWeatherData([FromBody] CreateWeatherDataDto dto)
    {
        var createdData = await repository.CreateWeatherDataAsync(dto);
        return Ok(ToGetDto(createdData));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllWeatherData()
    {
        var data = await repository.GetAllWeatherDataAsync();
        var result = data.Select(ToGetDto).ToList();

        return Ok(result);
    }

    [HttpGet, Route("latest/{location:guid}")]
    public async Task<IActionResult> GetLatestWeatherData([FromRoute] Guid location)
    {
        var data = await repository.GetLatestWeatherDataAsync(location);
        return data is null ? NotFound() : Ok(ToGetDto(data));
    }

    // We convert the Domain Model to a DTO to avoid exposing internal details
    // TODO - Move to Extension method
    private GetWeatherDataDto ToGetDto(WeatherData createdData) => new()
    {
        Id = createdData.Id,
        WeatherStationId = createdData.WeatherStationId,

        TemperatureCelsius = createdData.TemperatureCelsius,
        AirPressureHpa = createdData.AirPressureHpa,
        HumidityPercent = createdData.HumidityPercent,
        WindSpeedKph = createdData.WindSpeedKph,

        IsRaining = createdData.IsRaining,
        RainfallMm = createdData.RainfallMm,

        Timestamp = createdData.Timestamp
    };
}