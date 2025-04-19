using System.ComponentModel.DataAnnotations;

namespace Vinalyze_api
{
    public class Rating
    {
        public Guid Id { get; set; }

        // the account id of the user who made the rating
        public Guid AccountId { get; set; }

        // the wine id of the wine the rating is about
        public Guid WineId { get; set; }

        // the rating value, which must be between 1 and 5
        [Range(1, 5, ErrorMessage = "Value must be between 1 and 5.")]
        public int Value { get; set; }


    }
}
