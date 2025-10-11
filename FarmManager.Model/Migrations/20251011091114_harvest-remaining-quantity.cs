using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmManager.Model.Migrations
{
    /// <inheritdoc />
    public partial class harvestremainingquantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "RemainingCollectingQuantity",
                table: "Harvests",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "RemainingHourlyQuantity",
                table: "Harvests",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "RemainingQuantityAdditional",
                table: "Harvests",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "EmployeeCosts",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RemainingCollectingQuantity",
                table: "Harvests");

            migrationBuilder.DropColumn(
                name: "RemainingHourlyQuantity",
                table: "Harvests");

            migrationBuilder.DropColumn(
                name: "RemainingQuantityAdditional",
                table: "Harvests");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "EmployeeCosts");
        }
    }
}
