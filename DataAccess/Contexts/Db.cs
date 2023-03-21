#nullable disable

using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts
{
    public class Db : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<ProductShop> ProductShops { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }


        
        public Db(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductShop>().HasKey(ps => new { ps.ProductId,
             ps.ShopId});

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserDetails>()
                .HasOne(ud => ud.User)
                .WithOne(u => u.UserDetails)
                .HasForeignKey<UserDetails>(ud => ud.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<City>()
                .HasOne(city => city.Country)
                .WithMany(country => country.Cities)
                .OnDelete(DeleteBehavior.NoAction); 

            modelBuilder.Entity<UserDetails>()
                .HasOne(ud => ud.Country)
                .WithMany(c => c.UserDetails)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserDetails>()
                .HasOne(ud => ud.City)
                .WithMany(c => c.UserDetails)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>().HasIndex(u => u.UserName).IsUnique();
            

            modelBuilder.Entity<Product>().HasIndex(u => u.Name);
            


            

        }
    }
}
