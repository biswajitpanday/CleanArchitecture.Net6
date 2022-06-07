using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Repository.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherForecast",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WeatherCondition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TemperatureInFh = table.Column<long>(type: "bigint", nullable: false),
                    TemperatureInCelsius = table.Column<long>(type: "bigint", nullable: false),
                    RainProbability = table.Column<long>(type: "bigint", nullable: false),
                    WindDirection = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WindSpeed = table.Column<long>(type: "bigint", nullable: false),
                    Humidity = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherForecast", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherForecast");
        }
    }
}
