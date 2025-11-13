using FetchTheWeather.Backend.Service.Weather.Data;
using FetchTheWeather.Backend.Service.Weather.Models.Domain;
using FetchTheWeather.Backend.Service.Weather.Models.DTO.WeatherData;
using FetchTheWeather.Backend.Service.Weather.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FetchTheWeather.Backend.Service.Weather.Repositories;

public class WeatherDataRepository(WeatherDataContext context) : IWeatherDataRepository
{
    public async Task<WeatherData> CreateWeatherDataAsync(CreateWeatherDataDto data)
    {
        var weatherData = new WeatherData
        {
            WeatherStationId = data.WeatherStationId,

            TemperatureCelsius = data.TemperatureCelsius,
            AirPressureHpa = data.AirPressureHpa,
            HumidityPercent = data.HumidityPercent,
            WindSpeedKph = data.WindSpeedKph,

            IsRaining = data.IsRaining,
            RainfallMm = data.RainfallMm,

            Timestamp = data.Timestamp
        };

        context.WeatherData.Add(weatherData);
        await context.SaveChangesAsync();

        return weatherData;
    }

    public async Task<IEnumerable<WeatherData>> GetAllWeatherDataAsync()
        => await context.WeatherData.ToListAsync();

    public async Task<WeatherData?> GetLatestWeatherDataAsync(Guid stationId)
        => await context.WeatherData
            .Where(d => d.WeatherStationId == stationId)
            .OrderByDescending(d => d.Timestamp)
            .FirstOrDefaultAsync();
}