using FetchTheWeather.Backend.Service.Weather.Data;
using FetchTheWeather.Backend.Service.Weather.Mappers;
using FetchTheWeather.Backend.Service.Weather.Models.Domain;
using FetchTheWeather.Backend.Service.Weather.Models.DTO.WeatherData;
using FetchTheWeather.Backend.Service.Weather.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FetchTheWeather.Backend.Service.Weather.Repositories;

public class WeatherDataRepository(WeatherDataContext context) : IWeatherDataRepository
{
    public async Task<WeatherData> CreateWeatherDataAsync(CreateWeatherDataDto data)
    {
        var weatherData = data.ToDomain();

        context.WeatherData.Add(weatherData);
        await context.SaveChangesAsync();

        return weatherData;
    }

    public async Task<IEnumerable<WeatherData>> GetAllWeatherDataAsync()
        => await context.WeatherData.ToListAsync();

    public async Task<WeatherData?> GetLatestWeatherDataAsync(Guid stationId)
        => await context.WeatherData
            .Where(d => d.WeatherStationId == stationId)
            .OrderBy(d => d.Timestamp)
            .FirstOrDefaultAsync();
    
    public async Task<IEnumerable<WeatherData>> GetRangeWeatherDataAsync(DateOnly start, DateOnly end)
        => await context.WeatherData
            .Where (d => DateOnly.FromDateTime(d.Timestamp) >= start && DateOnly.FromDateTime(d.Timestamp) <= end)
            .OrderBy(d => d.Timestamp)
            .ToListAsync();
}