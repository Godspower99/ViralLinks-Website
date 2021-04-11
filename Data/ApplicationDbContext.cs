using System.Net.Mime;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ViralLinks.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<FileMetaData> FileMetaDatas { get; set; }
        public DbSet<SignUpForm> SignUpForms { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostLinkVisits> PostLinkVisits { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<PostCertificate> PostCertificates { get; set; }
        public DbSet<SavedPost> SavedPosts { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options){}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // default sub-routine
            base.OnModelCreating(builder);

            // add post categories
            builder.Entity<PostCategory>().HasData(
                // feeds for you
                new PostCategory {
                    Id = "all",
                    Header = "Feeds for you",
                    SubHeader = "Feeds customized using your activities.",
                    Description = "Keep enjoying your preferences on virallinks to keep updating your feeds for you.",
                    Position = 0,
                },

                // Online shops
                new PostCategory {
                    Id = "online_shops",
                    Header = "Online shops",
                    SubHeader = "Your Customers Are Waiting.",
                    Description = "Post your online shop links with the description of what you have to offer.",
                    Position = 1,
                },

                // Whatsapp groups
                new PostCategory {
                    Id = "whatsapp_groups",
                    Header = "WhatsApp groups",
                    SubHeader = "Your Members Are Here.",
                    Description = "Post your whatsapp group link with a brief description of what the group is about.",
                    Position = 2,
                },
                
                // Social media pages
                new PostCategory {
                    Id = "social_media_pages",
                    Header = "Social media pages",
                    SubHeader = "Get Those Likes, Shares and Followers Now.",
                    Description = "Post your Facebook, Instagram, Twitter and other social media page links.",
                    Position = 3,
                },

                // Professional Services
                new PostCategory {
                    Id = "professional_services",
                    Header = "Professional Services",
                    SubHeader = "Get Your Clients Now.",
                    Description = "Post the professional services you offer - Doctors, Lawyers, Architects, Engineers, Teachers, Artists, Designers, Accountants, Etc.",
                    Position = 4,
                },
                
                // Occasions and Events
                new PostCategory {
                    Id = "occasions_and_events",
                    Header = "Occasions and Events",
                    SubHeader = "We Will Turn Up Live.",
                    Description = "Post Information on your upcoming events - weddings, birthdays, crusades, shows, dedications, anniversaries, etc.",
                    Position = 5,
                },

                // YouTube Channels
                new PostCategory {
                    Id = "youtube_channels",
                    Header = "YouTube channels",
                    SubHeader = "Boost Your Views, Likes. Comments And Subscribers",
                    Description = "Post your YouTube channel links.",
                    Position = 6,
                },
                
                // Crypto And Forex
                new PostCategory {
                    Id = "crypto_and_forex",
                    Header = "Crypto And Forex",
                    SubHeader = "Lets Earn Digital Gold.",
                    Description = "Post your Crypto earning sites and Forex opportunities.",
                    Position = 7,
                },
                
                // Real Estate Business
                new PostCategory {
                    Id = "real_estate_business",
                    Header = "Real Estate Business",
                    SubHeader = "Make Sales Now.",
                    Description = "Post your lands, buildings, rooms, properties to let and to lease etc.",
                    Position = 8,
                },

                // Network Marketing
                new PostCategory {
                    Id = "network_marketing",
                    Header = "Network Marketing",
                    SubHeader = "Your Success Lines Are Here.",
                    Description = "Post your Network Marketing business with a brief description of the products and compensation plan.",
                    Position = 9,

                },

                // Jobs, Careers, Vacancies
                new PostCategory {
                    Id = "jobs_careers_vacancies",
                    Header = "Job, Careers, Vacancies",
                    SubHeader = "Employees Are Waiting.",
                    Description = "Post your job vacancies.",
                    Position = 10,
                },

                // Online Earning Platforms
                new PostCategory {
                    Id = "online_earning_platforms",
                    Header = "Online Earning Platforms",
                    SubHeader = "Let's earn with you",
                    Description = "Post confirmed paying sites with your proof of payments.",
                    Position = 11,
                }
            );
        }
    }
}