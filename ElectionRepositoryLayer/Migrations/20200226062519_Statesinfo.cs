using Microsoft.EntityFrameworkCore.Migrations;

namespace ElectionRepositoryLayer.Migrations
{
    public partial class Statesinfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Constituencies");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "Candidates",
                newName: "StateName");

            migrationBuilder.AddColumn<int>(
                name: "StateID",
                table: "Constituencies",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StateName",
                table: "Constituencies",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "StateID",
                table: "Candidates",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StateID",
                table: "Constituencies");

            migrationBuilder.DropColumn(
                name: "StateName",
                table: "Constituencies");

            migrationBuilder.DropColumn(
                name: "StateID",
                table: "Candidates");

            migrationBuilder.RenameColumn(
                name: "StateName",
                table: "Candidates",
                newName: "State");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Constituencies",
                nullable: true);
        }
    }
}
