using FetchTheWeather.Backend.Service.Weather.Data;
using FetchTheWeather.Backend.Service.Weather.Mappers;
using FetchTheWeather.Backend.Service.Weather.Models.Domain;
using FetchTheWeather.Backend.Service.Weather.Models.DTO.WeatherStation;
using FetchTheWeather.Backend.Service.Weather.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FetchTheWeather.Backend.Service.Weather.Repositories;

public class WeatherStationRepository(WeatherDataContext context) : IWeatherStationRepository
{
    public async Task<WeatherStation> CreateWeatherStationAsync(CreateWeatherStationDto station)
    {
        var weatherStation = station.ToDomain();

        context.WeatherStations.Add(weatherStation);
        await context.SaveChangesAsync();

        return weatherStation;
    }

    public async Task<IEnumerable<WeatherStation>> GetAllWeatherStationsAsync()
        => await context.WeatherStations.ToListAsync();

    public async Task<WeatherStation?> GetWeatherStationByIdAsync(Guid stationId)
        => await context.WeatherStations
            .FirstOrDefaultAsync(s => s.Id == stationId);
}