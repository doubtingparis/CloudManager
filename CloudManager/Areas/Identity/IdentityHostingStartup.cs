using System;
using CloudManager.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


// Settings already defined in StartUp.cs


[assembly: HostingStartup(typeof(CloudManager.Areas.Identity.IdentityHostingStartup))]
namespace CloudManager.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<IdentityDBContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("IdentityDBContext")));

                //services.AddDefaultIdentity<IdentityUser>()
                //    .AddEntityFrameworkStores<IdentityDBContext>();
            });
        }
    }
}