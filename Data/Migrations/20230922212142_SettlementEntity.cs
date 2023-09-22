using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMKancelariapp.Data.Migrations
{
    /// <inheritdoc />
    public partial class SettlementEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SettlementId",
                table: "Tasks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SettlementId",
                table: "HourlyPrices",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Settlements",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserTaskId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TotalPrice = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsSettled = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settlements", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_SettlementId",
                table: "Tasks",
                column: "SettlementId",
                unique: true,
                filter: "[SettlementId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_HourlyPrices_SettlementId",
                table: "HourlyPrices",
                column: "SettlementId",
                unique: true,
                filter: "[SettlementId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Settlements_UserTaskId",
                table: "Settlements",
                column: "UserTaskId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HourlyPrices_Settlements_SettlementId",
                table: "HourlyPrices",
                column: "SettlementId",
                principalTable: "Settlements",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Settlements_SettlementId",
                table: "Tasks",
                column: "SettlementId",
                principalTable: "Settlements",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HourlyPrices_Settlements_SettlementId",
                table: "HourlyPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Settlements_SettlementId",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "Settlements");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_SettlementId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_HourlyPrices_SettlementId",
                table: "HourlyPrices");

            migrationBuilder.DropColumn(
                name: "SettlementId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "SettlementId",
                table: "HourlyPrices");
        }
    }
}
