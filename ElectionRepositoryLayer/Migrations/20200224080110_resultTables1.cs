using Microsoft.EntityFrameworkCore.Migrations;

namespace ElectionRepositoryLayer.Migrations
{
    public partial class resultTables1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ResultID",
                table: "Result",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "ResultID",
                table: "PartywiseResults",
                newName: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Result",
                newName: "ResultID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "PartywiseResults",
                newName: "ResultID");
        }
    }
}
