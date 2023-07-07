using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMKancelariapp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedIdDataType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HourlyPrices_AspNetUsers_UserId",
                table: "HourlyPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_HourlyPrices_Tasks_Id",
                table: "HourlyPrices");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "TaskTypes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "TaskTypeId",
                table: "Tasks",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "Tasks",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CaseId",
                table: "Tasks",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Tasks",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "HourlyPriceId",
                table: "Tasks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "HourlyPrices",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "HourlyPrices",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "HourlyPrices",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "TaskTypeId",
                table: "HourlyPrices",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Clients",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "Cases",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Cases",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_HourlyPriceId",
                table: "Tasks",
                column: "HourlyPriceId");

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
                name: "FK_HourlyPrices_TaskTypes_TaskTypeId",
                table: "HourlyPrices",
                column: "TaskTypeId",
                principalTable: "TaskTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_HourlyPrices_HourlyPriceId",
                table: "Tasks",
                column: "HourlyPriceId",
                principalTable: "HourlyPrices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HourlyPrices_AspNetUsers_UserId",
                table: "HourlyPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_HourlyPrices_TaskTypes_TaskTypeId",
                table: "HourlyPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_HourlyPrices_HourlyPriceId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_HourlyPriceId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_HourlyPrices_TaskTypeId",
                table: "HourlyPrices");

            migrationBuilder.DropColumn(
                name: "HourlyPriceId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "TaskTypeId",
                table: "HourlyPrices");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "TaskTypes",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "TaskTypeId",
                table: "Tasks",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Tasks",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CaseId",
                table: "Tasks",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Tasks",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "HourlyPrices",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "HourlyPrices",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "HourlyPrices",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Clients",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Cases",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Cases",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_HourlyPrices_AspNetUsers_UserId",
                table: "HourlyPrices",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HourlyPrices_Tasks_Id",
                table: "HourlyPrices",
                column: "Id",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
