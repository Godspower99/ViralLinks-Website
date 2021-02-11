using Microsoft.EntityFrameworkCore.Migrations;

namespace ViralLinks.Migrations
{
    public partial class Updated_feeds_for_you_to_all : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PostCategories",
                keyColumn: "Id",
                keyValue: "feed_for_you");

            migrationBuilder.InsertData(
                table: "PostCategories",
                columns: new[] { "Id", "Description", "Header", "Position", "SubHeader" },
                values: new object[] { "all", "Keep enjoying your preferences on virallinks to keep updating your feeds for you.", "Feeds for you", 0, "Feeds customized using your activities." });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PostCategories",
                keyColumn: "Id",
                keyValue: "all");

            migrationBuilder.InsertData(
                table: "PostCategories",
                columns: new[] { "Id", "Description", "Header", "Position", "SubHeader" },
                values: new object[] { "feed_for_you", "Keep enjoying your preferences on virallinks to keep updating your feeds for you.", "Feeds for you", 0, "Feeds customized using your activities." });
        }
    }
}
