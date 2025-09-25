using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmManager.Model.Migrations
{
    /// <inheritdoc />
    public partial class modelAdjustments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sprayings_WorkDays_WorkDayId",
                table: "Sprayings");

            migrationBuilder.DropIndex(
                name: "IX_Sprayings_WorkDayId",
                table: "Sprayings");

            migrationBuilder.DropColumn(
                name: "WorkDayId",
                table: "Sprayings");

            migrationBuilder.AddColumn<bool>(
                name: "IsCollectingPayed",
                table: "WorkDays",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHourlyPayed",
                table: "WorkDays",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "ToPay",
                table: "Employees",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalEarned",
                table: "Employees",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCollectingPayed",
                table: "WorkDays");

            migrationBuilder.DropColumn(
                name: "IsHourlyPayed",
                table: "WorkDays");

            migrationBuilder.DropColumn(
                name: "ToPay",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "TotalEarned",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "WorkDayId",
                table: "Sprayings",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sprayings_WorkDayId",
                table: "Sprayings",
                column: "WorkDayId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sprayings_WorkDays_WorkDayId",
                table: "Sprayings",
                column: "WorkDayId",
                principalTable: "WorkDays",
                principalColumn: "Id");
        }
    }
}
