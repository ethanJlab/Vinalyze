using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vinalyze_api.Controllers.Data;
using System.Security.Cryptography;
using System.Text;

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
                return BadRequest("Object must not be null");

            if (newAccount.Password is null)
                return BadRequest("Password must not be null");

            // initialize SHA256 and string encoding 
            Encoding enc = Encoding.UTF8;
            SHA256 sha265Hash = SHA256.Create();

            // encript password as byte array
            byte[] rawEncriptedPassword = sha265Hash.ComputeHash(enc.GetBytes(newAccount.Password));
            
            // check if the encription failed
            if (rawEncriptedPassword is null)
                return BadRequest("Unable to hash password");

            // convert byte array to a string and save
            StringBuilder encriptedPassword = new StringBuilder();

            foreach (byte b in rawEncriptedPassword)
                encriptedPassword.Append(b.ToString("x2"));

            newAccount.Password = encriptedPassword.ToString();

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