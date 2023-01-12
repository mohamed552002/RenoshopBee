using Microsoft.EntityFrameworkCore;
using RenoshopBee.Models;

namespace RenoshopBee.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {

        }
        public DbSet<Product>Products { get; set; }
    }
}
