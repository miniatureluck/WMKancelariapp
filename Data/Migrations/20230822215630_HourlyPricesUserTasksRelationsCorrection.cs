using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMKancelariapp.Data.Migrations
{
    /// <inheritdoc />
    public partial class HourlyPricesUserTasksRelationsCorrection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_HourlyPrices_HourlyPriceId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_HourlyPriceId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "HourlyPriceId",
                table: "Tasks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HourlyPriceId",
                table: "Tasks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_HourlyPriceId",
                table: "Tasks",
                column: "HourlyPriceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_HourlyPrices_HourlyPriceId",
                table: "Tasks",
                column: "HourlyPriceId",
                principalTable: "HourlyPrices",
                principalColumn: "Id");
        }
    }
}
