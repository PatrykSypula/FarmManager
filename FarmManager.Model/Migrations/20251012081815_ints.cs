using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmManager.Model.Migrations
{
    /// <inheritdoc />
    public partial class ints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeCosts_Payments_PaymentId",
                table: "EmployeeCosts");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeCosts_PaymentId",
                table: "EmployeeCosts");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "EmployeeCosts");

            migrationBuilder.AddColumn<decimal>(
                name: "EmployeeCost",
                table: "Payments",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int[]>(
                name: "EmployeeCosts",
                table: "Payments",
                type: "integer[]",
                nullable: false,
                defaultValue: new int[0]);

            migrationBuilder.AddColumn<decimal>(
                name: "PaymentQuantity",
                table: "Payments",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeCost",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "EmployeeCosts",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PaymentQuantity",
                table: "Payments");

            migrationBuilder.AddColumn<int>(
                name: "PaymentId",
                table: "EmployeeCosts",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeCosts_PaymentId",
                table: "EmployeeCosts",
                column: "PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeCosts_Payments_PaymentId",
                table: "EmployeeCosts",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id");
        }
    }
}
