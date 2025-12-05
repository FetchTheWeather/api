using FetchTheWeather.Backend.Service.Weather.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FetchTheWeather.Backend.Service.Weather.Data;

public class WeatherDataContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<WeatherStation> WeatherStations { get; init; } = null!;
    public DbSet<WeatherData> WeatherData { get; init; } = null!;
}