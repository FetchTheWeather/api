using FetchTheWeather.Backend.Service.Weather.Models.Domain;
using FetchTheWeather.Backend.Service.Weather.Models.DTO.WeatherStation;

namespace FetchTheWeather.Backend.Service.Weather.Mappers;

public static class WeatherStationMapper
{
    public static WeatherStation ToDomain(this CreateWeatherStationDto source) => new()
    {
        Name = source.Name,
        Location = source.Location
    };

    public static GetWeatherStationDto ToGetDto(this WeatherStation source) => new()
    {
        Id = source.Id,
        Name = source.Name,
        Location = source.Location
    };
}