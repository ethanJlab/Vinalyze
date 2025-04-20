namespace Vinalyze_api
{
    public class Account
    {
        public Guid Id { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

        public string? Email { get; set; }

        // an array of wine ids to indicate liked wines
        public List<Guid>? LikedWines { get; set; }
    }
}