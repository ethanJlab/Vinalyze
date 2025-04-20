using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vinalyze_api.Controllers.Data;

namespace Vinalyze_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController(VinalyzeDbContext context) : ControllerBase
    {
        private readonly VinalyzeDbContext _context = context;

        // Get all ratings
        [HttpGet]
        public async Task<ActionResult<List<Rating>>> GetAllRatings()
        {
            return Ok(await _context.Rating.ToListAsync());
        }

        // Get ratings by wine id
        [HttpGet("wine/{wineId}")]
        public async Task<ActionResult<List<Rating>>> GetRatingsByWineId(Guid wineId)
        {
            var ratings = await _context.Rating.Where(r => r.WineId == wineId).ToListAsync();
            if (ratings == null || ratings.Count == 0)
            {
                return NotFound("No ratings found for this wine.");
            }
            return Ok(ratings);
        }

        // Get ratings by account id
        [HttpGet("account/{accountId}")]
        public async Task<ActionResult<List<Rating>>> GetRatingsByAccountId(Guid accountId)
        {
            var ratings = await _context.Rating.Where(r => r.AccountId == accountId).ToListAsync();
            if (ratings == null || ratings.Count == 0)
            {
                return NotFound("No ratings found for this account.");
            }
            return Ok(ratings);
        }

        public class CreateRatingModel
        {
            public Guid AccountId { get; set; }
            public Guid WineId { get; set; }
            public int Value { get; set; }
        }

        // Create a new rating
        [HttpPost]
        public async Task<ActionResult<Rating>> CreateRating([FromBody] CreateRatingModel model)
        {
            if (model == null)
            {
                return BadRequest("Rating cannot be null.");
            }
            var rating = new Rating
            {
                Id = Guid.NewGuid(),
                AccountId = model.AccountId,
                WineId = model.WineId,
                Value = model.Value
            };
            _context.Rating.Add(rating);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAllRatings), new { id = rating.Id }, rating);

        }

        public class UpdateRatingModel
        {
            public Guid Id { get; set; }
            public int? Value { get; set; }

        }
        // Update a rating
        [HttpPut]
        public async Task<IActionResult> UpdateRating([FromBody] UpdateRatingModel request)
        {
            var rating = await _context.Rating.FindAsync(request.Id);
            if (rating == null)
            {
                return NotFound("Rating not found.");
            }
            if (request.Value.HasValue)
            {
                if (request.Value.Value < 1 || request.Value.Value > 5)
                {
                    return BadRequest("Rating value must be between 1 and 5.");
                }
                rating.Value = request.Value.Value;
            }
            _context.Rating.Update(rating);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Delete a rating
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRating(Guid id)
        {
            var rating = await _context.Rating.FindAsync(id);
            if (rating == null)
            {
                return NotFound("Rating not found.");
            }
            _context.Rating.Remove(rating);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
