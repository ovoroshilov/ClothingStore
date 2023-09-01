using ClothingStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace ClothingStore.DAL
{
    public class StoreAppContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }

        public StoreAppContext(DbContextOptions<StoreAppContext> options) : base(options) => Database.EnsureCreated();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            });   
        }
    }
}
