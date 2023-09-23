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

            migrationBuilder.CreateTable(
                name: "Settlements",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TotalPrice = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsSettled = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settlements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Settlements_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_SettlementId",
                table: "Tasks",
                column: "SettlementId");

            migrationBuilder.CreateIndex(
                name: "IX_Settlements_ClientId",
                table: "Settlements",
                column: "ClientId");

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
                name: "FK_Tasks_Settlements_SettlementId",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "Settlements");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_SettlementId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "SettlementId",
                table: "Tasks");
        }
    }
}
