using System.Text;
using FetchTheWeather.Backend.Service.Auth.Data;
using FetchTheWeather.Backend.Service.Auth.Models;
using FetchTheWeather.Backend.Service.Auth.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

builder.Services.AddDbContext<AuthDataContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddIdentityCore<FtwUser>()
    .AddRoles<FtwRole>()
    .AddEntityFrameworkStores<AuthDataContext>();
builder.Services.AddIdentityApiEndpoints<FtwUser>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Authentication:Issuer"],
        ValidAudience = builder.Configuration["Authentication:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Authentication:SecretKey"])),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(corsBuilder =>
        {
            corsBuilder.WithOrigins("http://localhost:3000")
                .WithHeaders("Content-Type", "Authorization", "Accept", "Origin", "X-Requested-With",
                    "X-SignalR-User-Agent")
                .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS")
                .AllowCredentials();
        });
    });
}

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AuthDataContext>();

    if (!await context.Database.CanConnectAsync())
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError("Database connection failed, check your configuration.");

        return;
    }

    await context.Database.MigrateAsync();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    app.UseCors();
}

app.UseAuthentication();
app.UseAuthorization();

var identityGroup = app.MapGroup("/auth");
identityGroup.MapIdentityApi<FtwUser>();

app.MapControllers();

app.Run();

// docker run --name fetchtheweather-postgres_auth -e POSTGRES_USER=ftw_user -e POSTGRES_PASSWORD=ftw_password -e POSTGRES_DB=fetchtheweather_auth -p 8001:5432 -d postgres