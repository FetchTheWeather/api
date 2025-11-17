using FetchTheWeather.Backend.Service.Weather.Data;
using FetchTheWeather.Backend.Service.Weather.Options;
using FetchTheWeather.Backend.Service.Weather.Repositories;
using FetchTheWeather.Backend.Service.Weather.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var databaseOptions = builder.Configuration.GetSection("Database").Get<DatabaseOptions>();
if (databaseOptions is null) throw new InvalidOperationException("Database options are not configured.");

var connectionString = $"Server={databaseOptions.Host};" +
                       $"Port={databaseOptions.Port};" +
                       $"Database={databaseOptions.Database};" +
                       $"User Id={databaseOptions.User};" +
                       $"Password={databaseOptions.Password};";

builder.Services.AddDbContext<WeatherDataContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddScoped<IWeatherDataRepository, WeatherDataRepository>();
builder.Services.AddScoped<IWeatherStationRepository, WeatherStationRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapControllers();

app.Run();

// docker run --name fetchtheweather-postgres -e POSTGRES_USER=ftw_user -e POSTGRES_PASSWORD=ftw_password -e POSTGRES_DB=fetchtheweather -p 5450:5432 -d postgres