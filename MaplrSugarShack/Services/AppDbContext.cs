using MaplrSugarSnack.Models;
using Microsoft.EntityFrameworkCore;

namespace MaplrSugarShack.Services
{
    public class AppDbContext: DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Order> Orders { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductType>().HasData(
                new ProductType
                {
                    Id = 1,
                    Name = "Amber"
                },
                new ProductType
                {
                    Id = 2,
                    Name = "Dark"
                },
                new ProductType
                {
                    Id = 3,
                    Name = "Clear"
                }
                );

            var products = Enumerable.Range(1, 10).Select(i => new Product { Id = i, ProductTypeId = i % 3 + 1, Name = $"Syrup{i}", Description = $"Description {i}", Price = i*10 });
            modelBuilder.Entity<Product>().HasData(products);
            base.OnModelCreating(modelBuilder);
        }
    }
}
