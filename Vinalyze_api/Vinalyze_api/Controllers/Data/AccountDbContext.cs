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
                    Id = new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"),
                    Username = "admin",
                    Password = "admin",
                    Email = "admin@gmail.com"
                }
            );
        }
    }
}
