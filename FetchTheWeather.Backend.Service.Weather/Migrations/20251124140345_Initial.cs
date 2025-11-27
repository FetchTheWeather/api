using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FetchTheWeather.Backend.Service.Weather.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WeatherStationId = table.Column<Guid>(type: "uuid", nullable: false),
                    TemperatureCelsius = table.Column<float>(type: "real", nullable: false),
                    AirPressureHpa = table.Column<float>(type: "real", nullable: false),
                    HumidityPercent = table.Column<float>(type: "real", nullable: false),
                    WindSpeedKph = table.Column<float>(type: "real", nullable: false),
                    IsRaining = table.Column<bool>(type: "boolean", nullable: false),
                    RainfallMm = table.Column<float>(type: "real", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeatherStations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherStations", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "WeatherData",
                columns: new[] { "Id", "AirPressureHpa", "HumidityPercent", "IsRaining", "RainfallMm", "TemperatureCelsius", "Timestamp", "WeatherStationId", "WindSpeedKph" },
                values: new object[,]
                {
                    { new Guid("445f6cee-fd64-4693-b8ab-cf6cd21aa35c"), 40f, 40f, true, 50f, 200f, new DateTime(1979, 7, 29, 0, 35, 5, 0, DateTimeKind.Utc), new Guid("5a44cc62-4add-4e5c-896d-a2333b18af92"), 60f },
                    { new Guid("9105ff38-cb7d-4469-8eb7-0d5252277a90"), 220f, 60f, true, 20f, 250f, new DateTime(1979, 7, 30, 22, 35, 5, 0, DateTimeKind.Utc), new Guid("5a44cc62-4add-4e5c-896d-a2333b18af92"), 100f },
                    { new Guid("94076bcd-04b9-412b-83af-d83c99e5b841"), 200f, 50f, false, 0f, 260f, new DateTime(1979, 7, 28, 23, 35, 5, 0, DateTimeKind.Utc), new Guid("5a44cc62-4add-4e5c-896d-a2333b18af92"), 120f },
                    { new Guid("bbd52613-e281-435d-9f3c-b208a1f68ee3"), 200f, 40f, false, 0f, 275f, new DateTime(1979, 7, 28, 22, 35, 5, 0, DateTimeKind.Utc), new Guid("5a44cc62-4add-4e5c-896d-a2333b18af92"), 120f }
                });

            migrationBuilder.InsertData(
                table: "WeatherStations",
                columns: new[] { "Id", "Location", "Name" },
                values: new object[] { new Guid("5a44cc62-4add-4e5c-896d-a2333b18af92"), "Weather Station", "Weather Station" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherData");

            migrationBuilder.DropTable(
                name: "WeatherStations");
        }
    }
}
