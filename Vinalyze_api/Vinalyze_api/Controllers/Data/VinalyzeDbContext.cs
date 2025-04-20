using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Cryptography;

namespace Vinalyze_api.Controllers.Data
{
    public class VinalyzeDbContext(DbContextOptions<VinalyzeDbContext> options) : DbContext(options)
    {
        public DbSet<Account> Account => Set<Account>();
        public DbSet<Wine> Wine => Set<Wine>();

        public DbSet<Comment> Comment => Set<Comment>();

        public DbSet<Rating> Rating => Set<Rating>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>().HasData(
                new Account
                {
                    Id = new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"),
                    Username = "admin",
                    Password = this.hashPassword("admin"),
                    Email = "admin@gmail.com",
                    LikedWines = new List<Guid> { new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7") }
                }
            );
            modelBuilder.Entity<Wine>().HasData(
                new Wine
                {
                    // id, Name, Description, FlavorProfile
                    Id = new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7"),
                    Name = "Sample Wine",
                    Description = "This was created from a plant somewhere on planet Earth. It is somewhere between 1 day old and 100 years old.",
                    FlavorProfile = "This tastes like dirt and feet."
                }
            );
            modelBuilder.Entity<Comment>().HasData(
                new Comment
                {
                    Id = new Guid("d1f8b2c3-4e5f-6a7b-8c9d-e0f1a2b3c4d5"),
                    AccountId = new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"),
                    WineId = new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7"),
                    Text = "This wine is amazing! I love the flavor profile."
                }
            );
            modelBuilder.Entity<Rating>().HasData(
                new Rating
                {
                    Id = new Guid("e1f2a3b4-c5d6-7e8f-9a0b-1c2d3e4f5a6b"),
                    AccountId = new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"),
                    WineId = new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7"),
                    Value = 5
                }
            );

        }
        // function to hash a string password
        private string hashPassword(string password)
        {
            // initialize SHA256 and string encoding 
            Encoding enc = Encoding.UTF8;
            SHA256 sha256Hash = SHA256.Create();

            // encript password as byte array
            byte[] rawEncryptedPassword = sha256Hash.ComputeHash(enc.GetBytes(password));

            // convert byte array to a string and save
            StringBuilder encryptedPassword = new StringBuilder();

            foreach (byte b in rawEncryptedPassword)
                encryptedPassword.Append(b.ToString("x2"));

            string ret = encryptedPassword.ToString();
            return ret;

        }
    }
}
