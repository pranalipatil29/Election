using Microsoft.EntityFrameworkCore.Migrations;

namespace ElectionRepositoryLayer.Migrations
{
    public partial class resultTables2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PercentOfVotes",
                table: "Result");

            migrationBuilder.DropColumn(
                name: "Leading",
                table: "PartywiseResults");

            migrationBuilder.RenameColumn(
                name: "Total",
                table: "PartywiseResults",
                newName: "Loss");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Loss",
                table: "PartywiseResults",
                newName: "Total");

            migrationBuilder.AddColumn<float>(
                name: "PercentOfVotes",
                table: "Result",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "Leading",
                table: "PartywiseResults",
                nullable: false,
                defaultValue: 0);
        }
    }
}
