using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmManager.Model.Migrations
{
    /// <inheritdoc />
    public partial class workdayType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkdayType",
                table: "Workdays",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkdayType",
                table: "Workdays");
        }
    }
}
