using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ITPlanet.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDBModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "RegionType",
                table: "Regions");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "WeatherForecasts",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<long>(
                name: "WeatherId",
                table: "WeatherForecasts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "RegionTypeId",
                table: "Regions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "RegionTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Weathers_RegionId",
                table: "Weathers",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_WeatherForecasts_WeatherId",
                table: "WeatherForecasts",
                column: "WeatherId");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_RegionTypeId",
                table: "Regions",
                column: "RegionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Regions_RegionTypes_RegionTypeId",
                table: "Regions",
                column: "RegionTypeId",
                principalTable: "RegionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WeatherForecasts_Weathers_WeatherId",
                table: "WeatherForecasts",
                column: "WeatherId",
                principalTable: "Weathers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Weathers_Regions_RegionId",
                table: "Weathers",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Regions_RegionTypes_RegionTypeId",
                table: "Regions");

            migrationBuilder.DropForeignKey(
                name: "FK_WeatherForecasts_Weathers_WeatherId",
                table: "WeatherForecasts");

            migrationBuilder.DropForeignKey(
                name: "FK_Weathers_Regions_RegionId",
                table: "Weathers");

            migrationBuilder.DropTable(
                name: "RegionTypes");

            migrationBuilder.DropIndex(
                name: "IX_Weathers_RegionId",
                table: "Weathers");

            migrationBuilder.DropIndex(
                name: "IX_WeatherForecasts_WeatherId",
                table: "WeatherForecasts");

            migrationBuilder.DropIndex(
                name: "IX_Regions_RegionTypeId",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "WeatherId",
                table: "WeatherForecasts");

            migrationBuilder.DropColumn(
                name: "RegionTypeId",
                table: "Regions");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "WeatherForecasts",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<long>(
                name: "AccountId",
                table: "Regions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<List<long>>(
                name: "RegionType",
                table: "Regions",
                type: "bigint[]",
                nullable: false);
        }
    }
}
