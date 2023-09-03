using ClothingStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ClothingStore.Domain.Enums;
using ClothingStore.Domain.Helpers;

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
            modelBuilder.Entity<User>(builder =>
            {
                builder.HasData(new User
                {
                    Id = 1,
                    Login = "OVoroshilov",
                    Password = HashPasswordHelper.HashPassword("password"),
                    Role = UserRole.Admin
                });
                builder.ToTable("Users").HasKey(x => x.Id);

                builder.Property(x => x.Login).IsRequired().HasMaxLength(50);

                builder.Property(x => x.Id).ValueGeneratedOnAdd();
            });


            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            });
        }
    }
}
