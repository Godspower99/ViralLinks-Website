using System.Net.Mime;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ViralLinks.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<SignUpForm> SignUpForms { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options){}
    }
}