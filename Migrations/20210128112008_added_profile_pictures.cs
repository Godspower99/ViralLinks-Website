using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ViralLinks.Migrations
{
    public partial class added_profile_pictures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProfilePictures",
                columns: table => new
                {
                    UserID = table.Column<string>(nullable: false),
                    Extension = table.Column<string>(nullable: true),
                    LastUpdate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfilePictures", x => x.UserID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfilePictures");
        }
    }
}
