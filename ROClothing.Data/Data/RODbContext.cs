using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ROClothing.Models;

namespace ROClothing.Data.Data
{
    public class RODbContext : IdentityDbContext
    {
        public RODbContext(DbContextOptions<RODbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().Property(p => p.CreatedDate).HasColumnType("date");
            modelBuilder.Entity<Category>().Property(p => p.CreatedDate).HasColumnType("date");
            modelBuilder.Entity<ProductType>().Property(p => p.CreatedDate).HasColumnType("date");

        }



        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    }
}
