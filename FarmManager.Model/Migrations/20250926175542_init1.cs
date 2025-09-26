using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmManager.Model.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkdayCollecting_Workdays_WorkdayId",
                table: "WorkdayCollecting");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkdayHourly_Workdays_WorkdayId",
                table: "WorkdayHourly");

            migrationBuilder.AlterColumn<int>(
                name: "WorkdayId",
                table: "WorkdayHourly",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WorkdayId",
                table: "WorkdayCollecting",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkdayCollecting_Workdays_WorkdayId",
                table: "WorkdayCollecting",
                column: "WorkdayId",
                principalTable: "Workdays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkdayHourly_Workdays_WorkdayId",
                table: "WorkdayHourly",
                column: "WorkdayId",
                principalTable: "Workdays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkdayCollecting_Workdays_WorkdayId",
                table: "WorkdayCollecting");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkdayHourly_Workdays_WorkdayId",
                table: "WorkdayHourly");

            migrationBuilder.AlterColumn<int>(
                name: "WorkdayId",
                table: "WorkdayHourly",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "WorkdayId",
                table: "WorkdayCollecting",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkdayCollecting_Workdays_WorkdayId",
                table: "WorkdayCollecting",
                column: "WorkdayId",
                principalTable: "Workdays",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkdayHourly_Workdays_WorkdayId",
                table: "WorkdayHourly",
                column: "WorkdayId",
                principalTable: "Workdays",
                principalColumn: "Id");
        }
    }
}
