using FetchTheWeather.Backend.Service.Logging.Data;
using FetchTheWeather.Backend.Service.Logging.Repositories;
using FetchTheWeather.Backend.Service.Logging.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// TODO - Move to configuration
var connectionString = $"Server=localhost;" +
                       $"Port=5450;" +
                       $"Database=fetchtheweather-logging;" +
                       $"User Id=ftw_user;" +
                       $"Password=ftw_password;";

builder.Services.AddDbContext<LogDataContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddScoped<ILogRepository, LogRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapControllers();

app.Run();

// docker run --name fetchtheweather-postgres-logging -e POSTGRES_USER=ftw_user -e POSTGRES_PASSWORD=ftw_password -e POSTGRES_DB=fetchtheweather-logging -p 5450:5432 -d postgres