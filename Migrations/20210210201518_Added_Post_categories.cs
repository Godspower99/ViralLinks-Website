using Microsoft.EntityFrameworkCore.Migrations;

namespace ViralLinks.Migrations
{
    public partial class Added_Post_categories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostCategories",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Header = table.Column<string>(nullable: true),
                    SubHeader = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostCategories", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "PostCategories",
                columns: new[] { "Id", "Description", "Header", "SubHeader" },
                values: new object[,]
                {
                    { "feed_for_you", "Keep enjoying your preferences on virallinks to keep updating your feeds for you.", "Feeds for you", "Feeds customized using your activities." },
                    { "online_shops", "Post your online shop links with the description of what you have to offer.", "Online shops", "Your Customers Are Waiting." },
                    { "whatsapp_groups", "Post your whatsapp group link with a brief description of what the group is about.", "WhatsApp groups", "Your Members Are Here." },
                    { "social_media_pages", "Post your Facebook, Instagram, Twitter and other social media page links.", "Social media pages", "Get Those Likes, Shares and Followers Now." },
                    { "professional_services", "Post the professional services you offer - Doctors, Lawyers, Architects, Engineers, Teachers, Artists, Designers, Accountants, Etc.", "Professional Services", "Get Your Clients Now." },
                    { "occasions_and_events", "Post Information on your upcoming events - weddings, birthdays, crusades, shows, dedications, anniversaries, etc.", "Occasions and Events", "We Will Turn Up Live." },
                    { "youtube_channels", "Post your YouTube channel links.", "YouTube channels", "Boost Your Views, Likes. Comments And Subscribers" },
                    { "crypto_and_forex", "Post your Crypto earning sites and Forex opportunities.", "Crypto And Forex", "Lets Earn Digital Gold." },
                    { "real_estate_business", "Post your lands, buildings, rooms, properties to let and to lease etc.", "Real Estate Business", "Make Sales Now." },
                    { "network_marketing", "Post your Network Marketing business with a brief description of the products and compensation plan.", "Network Marketing", "Your Success Lines Are Here." },
                    { "jobs_careers_vacancies", "Post your job vacancies.", "Job, Careers, Vacancies", "Employees Are Waiting." },
                    { "online_earning_platforms", "Post confirmed paying sites with your proof of payments.", "Online Earning Platforms", "Let's earn with you" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostCategories");
        }
    }
}
