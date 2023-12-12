using Microsoft.EntityFrameworkCore;

namespace ForexTradingBotWebAPI.Database
{
    public class ApplicationUserDbContext : DbContext
    {
        public ApplicationUserDbContext(DbContextOptions<ApplicationUserDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the existing AspNetUsers table
            modelBuilder.Entity<BlazorApp.Data.ApplicationUser>().ToTable("AspNetUsers");
        }

        public DbSet<BlazorApp.Data.ApplicationUser> AspNetUsers { get; set; }

        // Add other DbSet properties for tables from Blazor App as needed
    }
}