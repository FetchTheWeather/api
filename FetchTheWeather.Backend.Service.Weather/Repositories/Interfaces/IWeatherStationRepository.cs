using FetchTheWeather.Backend.Service.Weather.Models.Domain;
using FetchTheWeather.Backend.Service.Weather.Models.DTO.WeatherStation;

namespace FetchTheWeather.Backend.Service.Weather.Repositories.Interfaces;

public interface IWeatherStationRepository
{
    Task<WeatherStation> CreateWeatherStationAsync(CreateWeatherStationDto station);
    Task<IEnumerable<WeatherStation>> GetAllWeatherStationsAsync();
    Task<WeatherStation?> GetWeatherStationByIdAsync(Guid stationId);
}