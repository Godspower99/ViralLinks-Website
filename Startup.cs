using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ViralLinks.Data;
using ViralLinks.InternalServices;

namespace ViralLinks
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            // add configuration options
            services.AddOptions();
            services.Configure<AzureStorageConfigOptions>(Configuration.GetSection(AzureStorageConfigOptions.Name));
            services.Configure<DatabaseConnectionOptions>(Configuration.GetSection(DatabaseConnectionOptions.Name));

            var connectionString = Configuration.GetValue<string>("DatabaseConnections:Database");

            // add DbContext 
            services.AddDbContext<ApplicationDbContext>(options => {
                options.UseSqlServer(connectionString, b => {
                    b.MigrationsAssembly("ViralLinks");
                    b.EnableRetryOnFailure(maxRetryCount: 5);
                    }
                );
            });

            // add aspnet identity for applicatoin users
            services.AddIdentity<ApplicationUser, IdentityRole>( options => {
                // password requirement configuration
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;

                // account lockout configuration
                options.Lockout.AllowedForNewUsers = false;
                
                // sign-in configuration
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail =  false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                // token provider configuration
                // user account configuration
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            // TODO :: REPLACE WITH CUSTOM TOKEN PROVIDERS
            .AddDefaultTokenProviders();

            services.Configure<DataProtectionTokenProviderOptions>(options => {
                options.TokenLifespan = TimeSpan.FromDays(1);
            });
            
            // TODO :: RE-CONFIGURE COOKIE
            services.ConfigureApplicationCookie(config => {
                config.AccessDeniedPath = "/home";
                config.LoginPath = "/account/sign-in";
                config.ClaimsIssuer = "virallinks.com"; 
                config.ReturnUrlParameter = "returnUrl";
            });

            // Register Services
            services.AddTransient<ICommunicationServices,CommunicationServices>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                //app.UseHsts();
            }
            
            this.AddRequiredRoles(roleManager).GetAwaiter().GetResult();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private async Task AddRequiredRoles(RoleManager<IdentityRole> roleManager)
        {
            // add member role
            if(!await roleManager.RoleExistsAsync(Roles.Member))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Member));
            }
            // add admin role
            if(! await roleManager.RoleExistsAsync(Roles.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
            }
        }
    }
}
