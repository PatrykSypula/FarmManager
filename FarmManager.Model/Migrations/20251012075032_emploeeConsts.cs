using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmManager.Model.Migrations
{
    /// <inheritdoc />
    public partial class emploeeConsts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
