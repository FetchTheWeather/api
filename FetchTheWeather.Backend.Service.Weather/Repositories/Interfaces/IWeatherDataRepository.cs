using FetchTheWeather.Backend.Service.Weather.Models.Domain;
using FetchTheWeather.Backend.Service.Weather.Models.DTO.WeatherData;

namespace FetchTheWeather.Backend.Service.Weather.Repositories.Interfaces;

public interface IWeatherDataRepository
{
    Task<WeatherData> CreateWeatherDataAsync(CreateWeatherDataDto data);
    Task<IEnumerable<WeatherData>> GetAllWeatherDataAsync();
    Task<WeatherData?> GetLatestWeatherDataAsync(Guid stationId);
    Task<IEnumerable<WeatherData>> GetRangeWeatherDataAsync(DateOnly start, DateOnly end);
}