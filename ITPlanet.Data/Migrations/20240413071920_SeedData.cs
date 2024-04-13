using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ITPlanet.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { 1, null, "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 1, 0, "60d19c36-d149-484d-89ae-66b5a993e7d7", "admin@example.com", true, "Admin", "Admin", false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEP/rkAw8sXmOEq61vmIRrml6kwl7FxKq/oP0t7BwbNuo24m2q/faHAsY/6DqsO5u5g==", null, true, "", false, "Admin" });

            migrationBuilder.InsertData(
                table: "RegionTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1L, "string1" },
                    { 2L, "string2" },
                    { 3L, "string3" },
                    { 4L, "string4" },
                    { 5L, "string5" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Latitude", "Longitude", "Name", "ParentRegion", "RegionTypeId", "UserId" },
                values: new object[,]
                {
                    { 1L, 22.219999999999999, 22.219999999999999, "Name1", "Name1", 1L, 1 },
                    { 2L, 33.329999999999998, 33.329999999999998, "Name2", "Name1", 1L, 1 },
                    { 3L, 44.439999999999998, 44.439999999999998, "Name3", "Name2", 2L, 1 },
                    { 4L, 55.549999999999997, 55.549999999999997, "Name4", "Name3", 3L, 1 },
                    { 5L, 66.659999999999997, 66.659999999999997, "Name5", "Name4", 3L, 1 }
                });

            migrationBuilder.InsertData(
                table: "WeatherForecasts",
                columns: new[] { "Id", "DateTime", "RegionId", "Temperature", "WeatherCondition" },
                values: new object[,]
                {
                    { 1L, new DateTime(2024, 4, 13, 7, 19, 20, 361, DateTimeKind.Utc).AddTicks(7332), 1L, 12.2f, "RAIN" },
                    { 2L, new DateTime(2024, 4, 13, 7, 19, 20, 361, DateTimeKind.Utc).AddTicks(7337), 2L, 17.2f, "RAIN" },
                    { 3L, new DateTime(2024, 4, 13, 7, 19, 20, 361, DateTimeKind.Utc).AddTicks(7338), 3L, 13.2f, "RAIN" },
                    { 4L, new DateTime(2024, 4, 13, 7, 19, 20, 361, DateTimeKind.Utc).AddTicks(7340), 4L, 12.2f, "RAIN" },
                    { 5L, new DateTime(2024, 4, 13, 7, 19, 20, 361, DateTimeKind.Utc).AddTicks(7341), 5L, 12.2f, "RAIN" }
                });

            migrationBuilder.InsertData(
                table: "Weathers",
                columns: new[] { "Id", "Humidity", "MeasurementDateTime", "PrecipitationAmount", "RegionId", "RegionName", "Temperature", "WeatherCondition", "WindSpeed" },
                values: new object[,]
                {
                    { 1L, 11.11f, new DateTime(2024, 4, 13, 7, 19, 20, 361, DateTimeKind.Utc).AddTicks(7297), 12f, 1L, "Name1", 12.1f, "RAIN", 15.2f },
                    { 2L, 22.22f, new DateTime(2024, 4, 13, 7, 19, 20, 361, DateTimeKind.Utc).AddTicks(7305), 13f, 2L, "Name2", 12.1f, "RAIN", 15.2f },
                    { 3L, 17.11f, new DateTime(2024, 4, 13, 7, 19, 20, 361, DateTimeKind.Utc).AddTicks(7307), 13f, 3L, "Name3", 14.1f, "RAIN", 15.2f },
                    { 4L, 17.11f, new DateTime(2024, 4, 13, 7, 19, 20, 361, DateTimeKind.Utc).AddTicks(7308), 12f, 4L, "Name4", 72.1f, "RAIN", 15.2f },
                    { 5L, 19.11f, new DateTime(2024, 4, 13, 7, 19, 20, 361, DateTimeKind.Utc).AddTicks(7309), 12f, 5L, "Name5", 12.1f, "RAIN", 15.2f }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 1 });

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
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1);

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
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1);

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
