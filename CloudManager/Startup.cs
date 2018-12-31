using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using CloudManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;

namespace CloudManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // Determines whether user consent for cookies is required
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Specify ASP.NET Core 2.2 version
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            // Add the DB references using dependency injection
            // ConnectionString found in appsettings.json
            // Customers & devices
            services.AddDbContext<CloudManagerContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("CloudManagerContext")));
            // Users/logins
            services.AddDbContext<IdentityDBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("IdentityDBContext")));


            // Use default identity scheme for easy integration, can be customized later
            services.AddDefaultIdentity<IdentityUser>().AddDefaultUI(UIFramework.Bootstrap4).AddEntityFrameworkStores<IdentityDBContext>();

            // Options & settings for Identity, if left untouched they default to stock settings
            services.Configure<IdentityOptions>(options =>
            {
                // Password validation settings
                // Requirements lowered for easy debugging/testing purposes
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Prevention for spam-bots and brute-force password crackers
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // Ensures no duplicate emails
                options.User.RequireUniqueEmail = true;

                // Ensures confirmed users only
                // options.SignIn.RequireConfirmedEmail = true;
            });
        }

        // This method gets called by the runtime 
        // Can edit this to configure the HTTP request pipeline and disable/enable site features
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Developer settings/environment to see exceptions
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            // Route to errorpage
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // Recommended for privacy policy
            app.UseCookiePolicy();

            // Keeps controllers unaccessible for anyone without a valid Identity
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                // Default page for site launch
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
