using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMKancelariapp.Data.Migrations
{
    /// <inheritdoc />
    public partial class HourlyPriceMakeover : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HourlyPrices_AspNetUsers_UserId",
                table: "HourlyPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_HourlyPrices_Clients_ClientId",
                table: "HourlyPrices");

            migrationBuilder.DropIndex(
                name: "IX_HourlyPrices_TaskTypeId",
                table: "HourlyPrices");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "HourlyPrices",
                newName: "CaseId");

            migrationBuilder.RenameIndex(
                name: "IX_HourlyPrices_ClientId",
                table: "HourlyPrices",
                newName: "IX_HourlyPrices_CaseId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TaskTypes",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "HourlyPrices",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "HourlyPrices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_HourlyPrices_TaskTypeId_CaseId",
                table: "HourlyPrices",
                columns: new[] { "TaskTypeId", "CaseId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HourlyPrices_AspNetUsers_UserId",
                table: "HourlyPrices",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HourlyPrices_Cases_CaseId",
                table: "HourlyPrices",
                column: "CaseId",
                principalTable: "Cases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HourlyPrices_AspNetUsers_UserId",
                table: "HourlyPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_HourlyPrices_Cases_CaseId",
                table: "HourlyPrices");

            migrationBuilder.DropIndex(
                name: "IX_HourlyPrices_TaskTypeId_CaseId",
                table: "HourlyPrices");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "HourlyPrices");

            migrationBuilder.RenameColumn(
                name: "CaseId",
                table: "HourlyPrices",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_HourlyPrices_CaseId",
                table: "HourlyPrices",
                newName: "IX_HourlyPrices_ClientId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TaskTypes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "HourlyPrices",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HourlyPrices_TaskTypeId",
                table: "HourlyPrices",
                column: "TaskTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_HourlyPrices_AspNetUsers_UserId",
                table: "HourlyPrices",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HourlyPrices_Clients_ClientId",
                table: "HourlyPrices",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
