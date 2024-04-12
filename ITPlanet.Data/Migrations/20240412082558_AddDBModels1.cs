using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ITPlanet.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDBModels1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RegionTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1L, "string" },
                    { 2L, "string" },
                    { 3L, "string" },
                    { 4L, "string" },
                    { 5L, "string" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Latitude", "Longitude", "Name", "ParentRegion", "RegionTypeId" },
                values: new object[,]
                {
                    { 1L, 22.219999999999999, 22.219999999999999, "Name1", "Name1", 1L },
                    { 2L, 33.329999999999998, 33.329999999999998, "Name2", "Name1", 1L },
                    { 3L, 44.439999999999998, 44.439999999999998, "Name3", "Name2", 2L },
                    { 4L, 55.549999999999997, 55.549999999999997, "Name4", "Name3", 3L },
                    { 5L, 66.659999999999997, 66.659999999999997, "Name5", "Name4", 3L }
                });

            migrationBuilder.InsertData(
                table: "Weathers",
                columns: new[] { "Id", "Humidity", "MeasurementDateTime", "PrecipitationAmount", "RegionId", "RegionName", "Temperature", "WeatherCondition", "WindSpeed" },
                values: new object[,]
                {
                    { 1L, 11.11f, new DateTime(12, 12, 12, 12, 12, 12, 0, DateTimeKind.Utc), 12f, 1L, "Name1", 12.1f, "RAIN", 15.2f },
                    { 2L, 22.22f, new DateTime(12, 12, 12, 12, 12, 12, 0, DateTimeKind.Utc), 13f, 2L, "Name2", 12.1f, "RAIN", 15.2f },
                    { 3L, 17.11f, new DateTime(12, 12, 12, 12, 12, 12, 0, DateTimeKind.Utc), 13f, 3L, "Name3", 14.1f, "RAIN", 15.2f },
                    { 4L, 17.11f, new DateTime(12, 12, 12, 12, 12, 12, 0, DateTimeKind.Utc), 12f, 4L, "Name4", 72.1f, "RAIN", 15.2f },
                    { 5L, 19.11f, new DateTime(12, 12, 12, 12, 12, 12, 0, DateTimeKind.Utc), 12f, 5L, "Name5", 12.1f, "RAIN", 15.2f }
                });

            migrationBuilder.InsertData(
                table: "WeatherForecasts",
                columns: new[] { "Id", "DateTime", "Temperature", "WeatherCondition", "WeatherId" },
                values: new object[,]
                {
                    { 1L, new DateTime(12, 12, 12, 12, 12, 12, 0, DateTimeKind.Utc), 12.2f, "RAIN", 1L },
                    { 2L, new DateTime(12, 12, 12, 12, 12, 12, 0, DateTimeKind.Utc), 17.2f, "RAIN", 2L },
                    { 3L, new DateTime(12, 12, 12, 12, 12, 12, 0, DateTimeKind.Utc), 13.2f, "RAIN", 3L },
                    { 4L, new DateTime(12, 12, 12, 12, 12, 12, 0, DateTimeKind.Utc), 12.2f, "RAIN", 4L },
                    { 5L, new DateTime(12, 12, 12, 12, 12, 12, 0, DateTimeKind.Utc), 12.2f, "RAIN", 5L }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RegionTypes",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "RegionTypes",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "WeatherForecasts",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "WeatherForecasts",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "WeatherForecasts",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "WeatherForecasts",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "WeatherForecasts",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Weathers",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Weathers",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Weathers",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Weathers",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Weathers",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "RegionTypes",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "RegionTypes",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "RegionTypes",
                keyColumn: "Id",
                keyValue: 3L);
        }
    }
}
