using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CloudManager.Models;

namespace CloudManager.Models
{
    public class CloudManagerContext : DbContext
    {
        public CloudManagerContext (DbContextOptions<CloudManagerContext> options) : base(options)
        {
        }

        //DB details in appsettings.json
        //Device DB link
        public DbSet<Device> Device { get; set; }

        //Customers DB link
        public DbSet<Customer> Customer { get; set; }
    }
}
