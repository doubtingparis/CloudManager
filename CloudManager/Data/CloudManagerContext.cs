using Microsoft.EntityFrameworkCore;

namespace CloudManager.Models
{
    public class CloudManagerContext : DbContext
    {
        public CloudManagerContext (DbContextOptions<CloudManagerContext> options) : base(options)
        {
        }
        //Device DB link
        public DbSet<Device> Device { get; set; }
        //Customers DB link
        public DbSet<Customer> Customer { get; set; }
    }
}
