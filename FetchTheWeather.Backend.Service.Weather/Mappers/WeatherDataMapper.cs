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
        AirQualityPpm = source.AirQualityPpm,
        HumidityPercent = source.HumidityPercent,

        Timestamp = UnixTimeStampToDateTime(source.Timestamp)
    };


    public static GetWeatherDataDto ToGetDto(this WeatherData source) => new()
    {
        Id = source.Id,
        WeatherStationId = source.WeatherStationId,

        TemperatureCelsius = source.TemperatureCelsius,
        AirPressureHpa = source.AirPressureHpa,
        AirQualityPpm = source.AirQualityPpm,
        HumidityPercent = source.HumidityPercent,

        Timestamp = source.Timestamp
    };

    private static DateTime UnixTimeStampToDateTime(long unixTimeStamp) =>
        DateTimeOffset.FromUnixTimeSeconds(unixTimeStamp).LocalDateTime;
}