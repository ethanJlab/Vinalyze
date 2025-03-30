using Microsoft.EntityFrameworkCore;

namespace Vinalyze_api.Controllers.Data
{
    public class WineDbContext(DbContextOptions<WineDbContext> options) : DbContext(options)
    {
        public DbSet<Wine> Wine => Set<Wine>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Wine>().HasData(
                new Wine
                {
                    // id, Name, Description, FlavorProfile
                    Id = 1,
                    Name = "Sample Wine",
                    Description = "This was created a plant somewhere on planet Earth. It is somewhere between 1 day old and 100 years old.",
                    FlavorProfile = "This tastes like dirt and feet."
                }
            );
        }
    }
}
