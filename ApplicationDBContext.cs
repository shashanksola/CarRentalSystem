using CarRentalSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Specify the precision and scale for PricePerDay
            modelBuilder.Entity<Car>()
                .Property(c => c.PricePerDay)
                .HasColumnType("decimal(18,2)"); // You can adjust precision and scale as needed
        }
    }
}
