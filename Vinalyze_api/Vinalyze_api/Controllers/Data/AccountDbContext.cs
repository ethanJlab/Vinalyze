using Microsoft.EntityFrameworkCore;

namespace Vinalyze_api.Controllers.Data
{
    public class AccountDbContext(DbContextOptions<AccountDbContext> options) : DbContext(options)
    {
        public DbSet<Account> Account => Set<Account>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>().HasData(
                new Account
                {
                    Id = 1,
                    Username = "admin",
                    Password = "admin",
                    Email = "admin@gmail.com"
                }
            );
        }
    }
}
