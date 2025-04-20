namespace Vinalyze_api
{
    public class Comment
    {
        public Guid Id { get; set; }

        // the account id of the user who made the comment
        public Guid AccountId { get; set; }

        // the wine id of the wine the comment is about
        public Guid WineId { get; set; }

        // the text of the comment
        public string? Text { get; set; }
    }
}
