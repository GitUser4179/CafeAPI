using Microsoft.EntityFrameworkCore;
using CafeAPI.Models;

namespace CafeAPI.Data
{
    public class CafeApiDbContext : DbContext
    {
        public CafeApiDbContext(DbContextOptions<CafeApiDbContext> options) : base(options)
        {
            
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
