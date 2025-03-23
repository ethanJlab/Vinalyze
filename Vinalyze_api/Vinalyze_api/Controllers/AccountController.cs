using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vinalyze_api.Controllers.Data;

namespace Vinalyze_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(AccountDbContext context) : ControllerBase
    {
        private readonly AccountDbContext _context = context;

        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(int id)
        {
            var account = await _context.Account.FindAsync(id);
            if (account is null)
                return NotFound();

            return Ok(account);
        }
    }
}