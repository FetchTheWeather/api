using FetchTheWeather.Backend.Service.Weather.Data;
using FetchTheWeather.Backend.Service.Weather.Models.Domain;

namespace FetchTheWeather.Backend.Service.Weather;

public class WeatherDataSeeder(WeatherDataContext context, ILogger<WeatherDataSeeder> logger)
{
    private static readonly List<Guid> StationIds =
    [
        Guid.Parse("11111111-1111-1111-1111-111111111111")
    ];

    public async Task<bool> ClearSeedDataAsync()
    {
        if (!context.WeatherData.Any()) return false;
        logger.LogInformation("Clearing seed data for stations: {Stations}", StationIds);

        foreach (var dataToRemove in StationIds.Select(stationId =>
                     context.WeatherData.Where(d => d.WeatherStationId == stationId)))
        {
            context.WeatherData.RemoveRange(dataToRemove);
        }

        await context.SaveChangesAsync();

        logger.LogInformation("Cleared seed data successfully");
        return true;
    }

    public async Task<bool> SeedAsync(int weeks = 1)
    {
        if (context.WeatherData.Any(d => StationIds.Contains(d.WeatherStationId))) return false;
        var seedData = new List<WeatherData>();

        var weekAgo = DateTime.UtcNow.AddDays(-7 * weeks);
        var minutesToGenerate = weeks * 7 * 24 * 60;

        logger.LogInformation("Seeding {Minutes} minutes of data for {Stations} stations", minutesToGenerate,
            StationIds.Count);

        foreach (var stationId in StationIds)
        {
            for (var i = 0; i < minutesToGenerate; i++)
            {
                seedData.Add(CreateRandomWeatherData(stationId, weekAgo, i));
            }
        }

        logger.LogInformation("Generated {Count} weather data entries", seedData.Count);
        context.WeatherData.AddRange(seedData);
        await context.SaveChangesAsync();

        logger.LogInformation("Seeded weather data successfully");
        return true;
    }

    private WeatherData CreateRandomWeatherData(Guid stationId, DateTime start, int index)
    {
        var random = new Random();
        return new WeatherData
        {
            WeatherStationId = stationId,
            TemperatureCelsius = (float)(random.NextDouble() * 40 - 10), // -10 to 30 °C
            AirPressureHpa = (float)(random.NextDouble() * 40 + 980), // 980 to 1020 hPa
            AirQualityPpm = (float)(random.NextDouble() * 200), // 0 to 200 ppm
            HumidityPercent = (float)(random.NextDouble() * 100), // 0 to 100 %
            Timestamp = start.AddMinutes(index)
        };
    }
}