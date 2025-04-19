using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vinalyze_api.Controllers.Data;
using System.Security.Cryptography;
using System.Text;
using Azure.Core;

namespace Vinalyze_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController( VinalyzeDbContext context) : ControllerBase
    {
        private readonly VinalyzeDbContext _context = context;

        // get all comments
        [HttpGet]
        public async Task<ActionResult<List<Comment>>> GetAllComments()
        {
            return Ok(await _context.Comment.ToListAsync());
        }

        // get comments by wine id
        [HttpGet("wine/{wineId}")]
        public async Task<ActionResult<List<Comment>>> GetCommentsByWineId(Guid wineId)
        {
            var comments = await _context.Comment.Where(c => c.WineId == wineId).ToListAsync();
            if (comments == null || comments.Count == 0)
            {
                return NotFound("No comments found for this wine.");
            }
            return Ok(comments);
        }

        // get comments by account id
        [HttpGet("account/{accountId}")]
        public async Task<ActionResult<List<Comment>>> GetCommentsByAccountId(Guid accountId)
        {
            var comments = await _context.Comment.Where(c => c.AccountId == accountId).ToListAsync();
            if (comments == null || comments.Count == 0)
            {
                return NotFound("No comments found for this account.");
            }
            return Ok(comments);
        }

        // create a new comment
        [HttpPost]
        public async Task<ActionResult<Comment>> CreateComment(Comment comment)
        {
            if (comment == null)
            {
                return BadRequest("Comment cannot be null.");
            }
            _context.Comment.Add(comment);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAllComments), new { id = comment.Id }, comment);
        }

        public class UpdateCommentModel
        {
            public string? Text { get; set; }
        }

        // update a comment
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(Guid id, [FromBody] UpdateCommentModel request)
        {
            var comment = await _context.Comment.FindAsync(id);
            if (comment == null)
            {
                return NotFound("Comment not found.");
            }
            if (request.Text != null)
            {
                comment.Text = request.Text;
            }
            _context.Entry(comment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // delete a comment
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            var comment = await _context.Comment.FindAsync(id);
            if (comment == null)
            {
                return NotFound("Comment not found.");
            }
            _context.Comment.Remove(comment);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
