using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElectionRepositoryLayer.Migrations
{
    public partial class States1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StateName",
                table: "States",
                type: "nvarchar(150)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "States",
                type: "DateTime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "States",
                type: "DateTime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "States");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "States");

            migrationBuilder.AlterColumn<string>(
                name: "StateName",
                table: "States",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)");
        }
    }
}
