using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMKancelariapp.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserTaskValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Value",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "Tasks");
        }
    }
}
