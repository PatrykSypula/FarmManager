using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FarmManager.Model.Migrations
{
    /// <inheritdoc />
    public partial class workdayAction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workdays_Harvests_HarvestId",
                table: "Workdays");

            migrationBuilder.DropForeignKey(
                name: "FK_Workdays_Plants_PlantId",
                table: "Workdays");

            migrationBuilder.AlterColumn<int>(
                name: "PlantId",
                table: "Workdays",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "HarvestId",
                table: "Workdays",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "ActionId",
                table: "Workdays",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Action",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Action", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Workdays_ActionId",
                table: "Workdays",
                column: "ActionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workdays_Action_ActionId",
                table: "Workdays",
                column: "ActionId",
                principalTable: "Action",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Workdays_Harvests_HarvestId",
                table: "Workdays",
                column: "HarvestId",
                principalTable: "Harvests",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Workdays_Plants_PlantId",
                table: "Workdays",
                column: "PlantId",
                principalTable: "Plants",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workdays_Action_ActionId",
                table: "Workdays");

            migrationBuilder.DropForeignKey(
                name: "FK_Workdays_Harvests_HarvestId",
                table: "Workdays");

            migrationBuilder.DropForeignKey(
                name: "FK_Workdays_Plants_PlantId",
                table: "Workdays");

            migrationBuilder.DropTable(
                name: "Action");

            migrationBuilder.DropIndex(
                name: "IX_Workdays_ActionId",
                table: "Workdays");

            migrationBuilder.DropColumn(
                name: "ActionId",
                table: "Workdays");

            migrationBuilder.AlterColumn<int>(
                name: "PlantId",
                table: "Workdays",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HarvestId",
                table: "Workdays",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Workdays_Harvests_HarvestId",
                table: "Workdays",
                column: "HarvestId",
                principalTable: "Harvests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workdays_Plants_PlantId",
                table: "Workdays",
                column: "PlantId",
                principalTable: "Plants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
