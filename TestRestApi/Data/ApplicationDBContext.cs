using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestRestApi.Data.Models;

namespace TestRestApi.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {



        }


        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders{ get; set; }
        public DbSet<Item> Order { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>(entity => {
                entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

               } );
            modelBuilder.Entity<Category>()
                .HasData(
               new Category { Id = 1, Name = "Electronics" },
                    new Category { Id = 2, Name = "Clothes" },
                    new Category { Id = 3, Name = "Books" }
                );
            
            
               modelBuilder.Entity<Item>()
              .HasOne(i => i.category)            // Item → Category navigation
              .WithMany(c => c.Items)             // Category → Items navigation
              .HasForeignKey(i => i.CategoryId);

            modelBuilder.Entity<Item>()
                .Property(i => i.Name).HasMaxLength(100);

        }
    }
}