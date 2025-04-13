using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vinalyze_api.Controllers.Data;

namespace Vinalyze_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WineController(VinalyzeDbContext context) : ControllerBase
    {
        private readonly VinalyzeDbContext _context = context;

        // get all wines
        [HttpGet]
        public async Task<ActionResult<List<Wine>>> GetAllWines()
        {
            return Ok(await _context.Wine.ToListAsync());
        }

        // get a wine by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Wine>> GetWine(Guid id)
        {
            var wine = await _context.Wine.FindAsync(id);
            if (wine is null)
                return NotFound();

            return Ok(wine);
        }

        // create a wine
        [HttpPost]
        public async Task<ActionResult<Wine>> CreateWine(Wine newWine)
        {
            if (newWine is null)
                return BadRequest();

            _context.Wine.Add(newWine);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetWine), new { id = newWine.Id }, newWine);
        }

        // update wine
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWine(Guid id, Wine updatedWine)
        {
            var existingWine = await _context.Wine.FindAsync(id);
            if (existingWine is null)
                return NotFound();

            existingWine.Name = updatedWine.Name;
            existingWine.Description = updatedWine.Description;
            existingWine.FlavorProfile = updatedWine.FlavorProfile;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // delete wine
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWine(Guid id)
        {
            var existingWine = await _context.Wine.FindAsync(id);
            if (existingWine is null)
                return NotFound();

            _context.Wine.Remove(existingWine);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
