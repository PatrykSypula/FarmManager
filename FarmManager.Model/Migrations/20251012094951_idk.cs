using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmManager.Model.Migrations
{
    /// <inheritdoc />
    public partial class idk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "SellHarvestQuantitys");

            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                table: "Sells",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Date",
                table: "Sells",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Sells",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlantId",
                table: "Sells",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "CollectingQuantity",
                table: "SellHarvestQuantitys",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CollectingQuantityAdditional",
                table: "SellHarvestQuantitys",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "HourlyQuantity",
                table: "SellHarvestQuantitys",
                type: "numeric",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sells_PlantId",
                table: "Sells",
                column: "PlantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sells_Plants_PlantId",
                table: "Sells",
                column: "PlantId",
                principalTable: "Plants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sells_Plants_PlantId",
                table: "Sells");

            migrationBuilder.DropIndex(
                name: "IX_Sells_PlantId",
                table: "Sells");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Sells");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Sells");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "Sells");

            migrationBuilder.DropColumn(
                name: "CollectingQuantity",
                table: "SellHarvestQuantitys");

            migrationBuilder.DropColumn(
                name: "CollectingQuantityAdditional",
                table: "SellHarvestQuantitys");

            migrationBuilder.DropColumn(
                name: "HourlyQuantity",
                table: "SellHarvestQuantitys");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Sells",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddColumn<decimal>(
                name: "Quantity",
                table: "SellHarvestQuantitys",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
