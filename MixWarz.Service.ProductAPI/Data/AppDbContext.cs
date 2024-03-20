using Microsoft.EntityFrameworkCore;
using MixWarz.Service.ProductAPI.Models;

namespace MixWarz.Service.ProductAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 1,
                Name = "Banger Drum Kit",
                Price = 15,
                Description = "Drum kit fulling of hard hitting kicks, snares, and crispy Hi-hats.",               
                CategoryName = "Sampled Drum Kits"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 2,
                Name = "Jazzy Brass",
                Price = 13.99,
                Description = "Dry sampled brass one shots and riffs.",               
                CategoryName = "Sampled Instruments"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 3,
                Name = "Catchy melodies",
                Price = 10.99,
                Description = "Catchy audio and midi melodies.",               
                CategoryName = "Melody Kits"
            });            
        }
    }
}
