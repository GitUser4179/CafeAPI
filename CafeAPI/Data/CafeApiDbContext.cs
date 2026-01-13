using Microsoft.EntityFrameworkCore;
using CafeAPI.Models;

namespace CafeAPI.Data
{
    public class CafeApiDbContext : DbContext
    {
        public CafeApiDbContext(DbContextOptions<CafeApiDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(p => p.Name)
                .IsRequired();

                entity.Property(p => p.Price)
                .IsRequired();

                entity.Property(p => p.CategoryId)
                .IsRequired();

                entity.HasIndex(p => p.Name)
                .IsUnique();
            });

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Coffee"},
                new Category { Id = 2, Name = "Tea"},
                new Category { Id = 3, Name = "Bakeries"}
                );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Espresso", Price = 25, CategoryId = 1 },
                new Product { Id = 2, Name = "Cappuccino", Price = 35, CategoryId = 1 },
                new Product { Id = 3, Name = "Latte", Price = 40, CategoryId = 1 },
                new Product { Id = 4, Name = "Earl Grey Tea", Price = 30, CategoryId = 2 },
                new Product { Id = 5, Name = "Green Tea", Price = 30, CategoryId = 2 },
                new Product { Id = 6, Name = "Cinnamon Bun", Price = 25, CategoryId = 3 },
                new Product { Id = 7, Name = "Croissant", Price = 28, CategoryId = 3 },
                new Product { Id = 8, Name = "Americano", Price = 30, CategoryId = 1 }
            );
        }   

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
