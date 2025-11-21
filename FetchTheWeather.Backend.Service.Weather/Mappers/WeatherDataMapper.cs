using FetchTheWeather.Backend.Service.Weather.Models.Domain;
using FetchTheWeather.Backend.Service.Weather.Models.DTO.WeatherData;

namespace FetchTheWeather.Backend.Service.Weather.Mappers;

public static class WeatherDataMapper
{
    public static WeatherData ToDomain(this CreateWeatherDataDto source) => new()
    {
        WeatherStationId = source.WeatherStationId,

        TemperatureCelsius = source.TemperatureCelsius,
        AirPressureHpa = source.AirPressureHpa,
        HumidityPercent = source.HumidityPercent,
        WindSpeedKph = source.WindSpeedKph,

        IsRaining = source.IsRaining,
        RainfallMm = source.RainfallMm,

        Timestamp = source.Timestamp,
    };

    public static GetWeatherDataDto ToGetDto(this WeatherData source) => new()
    {
        Id = source.Id,
        WeatherStationId = source.WeatherStationId,

        TemperatureCelsius = source.TemperatureCelsius,
        AirPressureHpa = source.AirPressureHpa,
        HumidityPercent = source.HumidityPercent,
        WindSpeedKph = source.WindSpeedKph,

        IsRaining = source.IsRaining,
        RainfallMm = source.RainfallMm,

        Timestamp = source.Timestamp
    };
}