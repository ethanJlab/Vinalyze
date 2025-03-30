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


        // get all accounts
        [HttpGet]
        public async Task<ActionResult<List<Account>>> GetAllAccounts()
        {
            return Ok(await _context.Account.ToListAsync());
        }

        // get an account by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(Guid id)
        {
            var account = await _context.Account.FindAsync(id);
            if (account is null)
                return NotFound();

            return Ok(account);
        }

        // create account
        [HttpPost]
        public async Task<ActionResult<Account>> CreateAccount(Account newAccount)
        {
            if (newAccount is null)
                return BadRequest();
            _context.Account.Add(newAccount);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAccount), new { id = newAccount.Id }, newAccount);
        }

        // update account
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(Guid id, Account updatedAccount)
        {
            var curAccount = await _context.Account.FindAsync(id);
            if (curAccount is null)
                return NotFound();

            curAccount.Username = updatedAccount.Username;
            curAccount.Password = updatedAccount.Password;
            curAccount.Email = updatedAccount.Email;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // delete account
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(Guid id)
        {
            var curAccount = await _context.Account.FindAsync(id);
            if (curAccount is null)
                return NotFound();

            _context.Account.Remove(curAccount);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}