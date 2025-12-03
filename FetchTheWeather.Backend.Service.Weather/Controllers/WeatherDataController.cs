using FetchTheWeather.Backend.Service.Weather.Mappers;
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
        return Ok(createdData.ToGetDto());
    }

    [HttpGet]
    public async Task<IActionResult> GetAllWeatherData()
    {
        var data = await repository.GetAllWeatherDataAsync();
        var result = data.Select(weatherData => weatherData.ToGetDto());

        return Ok(result);
    }

    [HttpGet, Route("latest/{location:guid}")]
    public async Task<IActionResult> GetLatestWeatherData([FromRoute] Guid location)
    {
        var data = await repository.GetLatestWeatherDataAsync(location);
        return data is null ? NotFound() : Ok(data.ToGetDto());
    }

    [HttpGet, Route("range")]
    public async Task<IActionResult> GetRangeWeatherData([FromQuery]DateOnly start,[FromQuery] DateOnly end)
    {
        var data = await repository.GetRangeWeatherDataAsync(start, end);
        var result = data.Select(x => x.ToGetDto());
        
        return Ok(result);
    }
}