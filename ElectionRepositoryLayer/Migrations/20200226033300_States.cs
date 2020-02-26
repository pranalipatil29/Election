using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElectionRepositoryLayer.Migrations
{
    public partial class States : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Constituencies",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)");

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    StateID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StateName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.StateID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Constituencies",
                type: "nvarchar(150)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
