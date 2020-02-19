﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace ElectionRepositoryLayer.Migrations
{
    public partial class electionDB1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VoterID",
                table: "AspNetUsers",
                type: "nvarchar(150)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VoterID",
                table: "AspNetUsers");
        }
    }
}
