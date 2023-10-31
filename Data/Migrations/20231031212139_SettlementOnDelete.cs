using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMKancelariapp.Data.Migrations
{
    /// <inheritdoc />
    public partial class SettlementOnDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Clients_ClientId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Settlements_SettlementId",
                table: "Tasks");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Clients_ClientId",
                table: "Tasks",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Settlements_SettlementId",
                table: "Tasks",
                column: "SettlementId",
                principalTable: "Settlements",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Clients_ClientId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Settlements_SettlementId",
                table: "Tasks");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Clients_ClientId",
                table: "Tasks",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Settlements_SettlementId",
                table: "Tasks",
                column: "SettlementId",
                principalTable: "Settlements",
                principalColumn: "Id");
        }
    }
}
