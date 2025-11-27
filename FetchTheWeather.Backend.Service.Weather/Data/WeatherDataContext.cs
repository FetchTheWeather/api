using System.Globalization;
using FetchTheWeather.Backend.Service.Weather.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace FetchTheWeather.Backend.Service.Weather.Data;

public class WeatherDataContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<WeatherStation> WeatherStations { get; init; } = null!;
    public DbSet<WeatherData> WeatherData { get; init; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var stationId = Guid.NewGuid();

        modelBuilder.Entity<WeatherStation>().HasData(new WeatherStation
        {
            Id = stationId,
            Name = "Weather Station",
            Location = "Weather Station"
        });

        modelBuilder.Entity<WeatherData>().HasData(new WeatherData
        {
            Id = Guid.NewGuid(),
            WeatherStationId = stationId,
            TemperatureCelsius = 275,
            AirPressureHpa = 200,
            HumidityPercent = 40,
            WindSpeedKph = 120,
            IsRaining = false,
            RainfallMm = 0,
            Timestamp = new DateTime(1979, 07, 28, 22, 35, 5, DateTimeKind.Utc)
        });

        modelBuilder.Entity<WeatherData>().HasData(new WeatherData
        {
            Id = Guid.NewGuid(),
            WeatherStationId = stationId,
            TemperatureCelsius = 260,
            AirPressureHpa = 200,
            HumidityPercent = 50,
            WindSpeedKph = 120,
            IsRaining = false,
            RainfallMm = 0,
            Timestamp = new DateTime(1979, 07, 28, 23, 35, 5, DateTimeKind.Utc)
        });
        modelBuilder.Entity<WeatherData>().HasData(new WeatherData
        {
            Id = Guid.NewGuid(),
            WeatherStationId = stationId,
            TemperatureCelsius = 200,
            AirPressureHpa = 40,
            HumidityPercent = 40,
            WindSpeedKph = 60,
            IsRaining = true,
            RainfallMm = 50,
            Timestamp = new DateTime(1979, 07, 29, 0, 35, 5, DateTimeKind.Utc)
        });
        
        modelBuilder.Entity<WeatherData>().HasData(new WeatherData
        {
            Id = Guid.NewGuid(),
            WeatherStationId = stationId,
            TemperatureCelsius = 250,
            AirPressureHpa = 220,
            HumidityPercent = 60,
            WindSpeedKph = 100,
            IsRaining = true,
            RainfallMm = 20,
            Timestamp = new DateTime(1979, 07, 30, 22, 35, 5, DateTimeKind.Utc)
        });
    }
}