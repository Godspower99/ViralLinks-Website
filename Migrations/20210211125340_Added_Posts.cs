using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ViralLinks.Migrations
{
    public partial class Added_Posts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CategoryId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    PostLink = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "PostCategories",
                keyColumn: "Id",
                keyValue: "crypto_and_forex",
                column: "Position",
                value: 7);

            migrationBuilder.UpdateData(
                table: "PostCategories",
                keyColumn: "Id",
                keyValue: "jobs_careers_vacancies",
                column: "Position",
                value: 10);

            migrationBuilder.UpdateData(
                table: "PostCategories",
                keyColumn: "Id",
                keyValue: "network_marketing",
                column: "Position",
                value: 9);

            migrationBuilder.UpdateData(
                table: "PostCategories",
                keyColumn: "Id",
                keyValue: "occasions_and_events",
                column: "Position",
                value: 5);

            migrationBuilder.UpdateData(
                table: "PostCategories",
                keyColumn: "Id",
                keyValue: "online_earning_platforms",
                column: "Position",
                value: 11);

            migrationBuilder.UpdateData(
                table: "PostCategories",
                keyColumn: "Id",
                keyValue: "online_shops",
                column: "Position",
                value: 1);

            migrationBuilder.UpdateData(
                table: "PostCategories",
                keyColumn: "Id",
                keyValue: "professional_services",
                column: "Position",
                value: 4);

            migrationBuilder.UpdateData(
                table: "PostCategories",
                keyColumn: "Id",
                keyValue: "real_estate_business",
                column: "Position",
                value: 8);

            migrationBuilder.UpdateData(
                table: "PostCategories",
                keyColumn: "Id",
                keyValue: "social_media_pages",
                column: "Position",
                value: 3);

            migrationBuilder.UpdateData(
                table: "PostCategories",
                keyColumn: "Id",
                keyValue: "whatsapp_groups",
                column: "Position",
                value: 2);

            migrationBuilder.UpdateData(
                table: "PostCategories",
                keyColumn: "Id",
                keyValue: "youtube_channels",
                column: "Position",
                value: 6);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.UpdateData(
                table: "PostCategories",
                keyColumn: "Id",
                keyValue: "crypto_and_forex",
                column: "Position",
                value: 0);

            migrationBuilder.UpdateData(
                table: "PostCategories",
                keyColumn: "Id",
                keyValue: "jobs_careers_vacancies",
                column: "Position",
                value: 0);

            migrationBuilder.UpdateData(
                table: "PostCategories",
                keyColumn: "Id",
                keyValue: "network_marketing",
                column: "Position",
                value: 0);

            migrationBuilder.UpdateData(
                table: "PostCategories",
                keyColumn: "Id",
                keyValue: "occasions_and_events",
                column: "Position",
                value: 0);

            migrationBuilder.UpdateData(
                table: "PostCategories",
                keyColumn: "Id",
                keyValue: "online_earning_platforms",
                column: "Position",
                value: 0);

            migrationBuilder.UpdateData(
                table: "PostCategories",
                keyColumn: "Id",
                keyValue: "online_shops",
                column: "Position",
                value: 0);

            migrationBuilder.UpdateData(
                table: "PostCategories",
                keyColumn: "Id",
                keyValue: "professional_services",
                column: "Position",
                value: 0);

            migrationBuilder.UpdateData(
                table: "PostCategories",
                keyColumn: "Id",
                keyValue: "real_estate_business",
                column: "Position",
                value: 0);

            migrationBuilder.UpdateData(
                table: "PostCategories",
                keyColumn: "Id",
                keyValue: "social_media_pages",
                column: "Position",
                value: 0);

            migrationBuilder.UpdateData(
                table: "PostCategories",
                keyColumn: "Id",
                keyValue: "whatsapp_groups",
                column: "Position",
                value: 0);

            migrationBuilder.UpdateData(
                table: "PostCategories",
                keyColumn: "Id",
                keyValue: "youtube_channels",
                column: "Position",
                value: 0);
        }
    }
}
