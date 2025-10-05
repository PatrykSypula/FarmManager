using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmManager.Model.Migrations
{
    /// <inheritdoc />
    public partial class workdayAction2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workdays_Action_ActionId",
                table: "Workdays");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Action",
                table: "Action");

            migrationBuilder.RenameTable(
                name: "Action",
                newName: "Actions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Actions",
                table: "Actions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Workdays_Actions_ActionId",
                table: "Workdays",
                column: "ActionId",
                principalTable: "Actions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workdays_Actions_ActionId",
                table: "Workdays");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Actions",
                table: "Actions");

            migrationBuilder.RenameTable(
                name: "Actions",
                newName: "Action");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Action",
                table: "Action",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Workdays_Action_ActionId",
                table: "Workdays",
                column: "ActionId",
                principalTable: "Action",
                principalColumn: "Id");
        }
    }
}
