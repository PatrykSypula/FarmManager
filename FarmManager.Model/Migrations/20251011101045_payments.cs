using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmManager.Model.Migrations
{
    /// <inheritdoc />
    public partial class payments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sells_Harvests_HarvestId",
                table: "Sells");

            migrationBuilder.DropIndex(
                name: "IX_Sells_HarvestId",
                table: "Sells");

            migrationBuilder.DropColumn(
                name: "HarvestId",
                table: "Sells");

            migrationBuilder.RenameColumn(
                name: "Buy",
                table: "SprayingBuyQuantitys",
                newName: "BuyId");

            migrationBuilder.RenameColumn(
                name: "Plant",
                table: "SellHarvestQuantitys",
                newName: "HarvestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BuyId",
                table: "SprayingBuyQuantitys",
                newName: "Buy");

            migrationBuilder.RenameColumn(
                name: "HarvestId",
                table: "SellHarvestQuantitys",
                newName: "Plant");

            migrationBuilder.AddColumn<int>(
                name: "HarvestId",
                table: "Sells",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sells_HarvestId",
                table: "Sells",
                column: "HarvestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sells_Harvests_HarvestId",
                table: "Sells",
                column: "HarvestId",
                principalTable: "Harvests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
