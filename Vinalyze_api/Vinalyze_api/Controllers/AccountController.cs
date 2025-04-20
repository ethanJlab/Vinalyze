using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vinalyze_api.Controllers.Data;
using System.Security.Cryptography;
using System.Text;
using System.Security.Principal;

namespace Vinalyze_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(VinalyzeDbContext context) : ControllerBase
    {
        private readonly VinalyzeDbContext _context = context;


        // get all accounts
        [HttpGet]
        public async Task<ActionResult<List<Account>>> GetAllAccounts()
        {
            return Ok(await _context.Account.ToListAsync());
        }

        // get an account by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>>  GetAccount(Guid id)
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

            newAccount.Password = this.hashPassword(newAccount.Password);

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

            if (updatedAccount.Password is null)
                return BadRequest("Password must not be null");

            curAccount.Username = updatedAccount.Username;
            curAccount.Email = updatedAccount.Email;
            curAccount.LikedWines = updatedAccount.LikedWines;

            // hash the password
            curAccount.Password = this.hashPassword(updatedAccount.Password);
            
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

        // login account
        [HttpPost("login/{id}")]
        public async Task<ActionResult<bool>> LoginAccount(Guid id, Account curAccount)
        {
            // pull the account from DB
            var account = await _context.Account.FindAsync(id);
            if (account is null)
                return NotFound();

            if (curAccount.Password is null)
                return BadRequest("Password must not be null");

            var hashedCurAccountPass = this.hashPassword(curAccount.Password);
            if (hashedCurAccountPass.Equals(account.Password))
                return Ok(true);
            return Ok(false);
        }


        public class WineToLikesRequest
        {
            public Guid accountId { get; set; }
            public Guid wineId { get; set; }
        }
        // add liked wine to account
        [HttpPost("addWineToLikes")]
        public async Task<IActionResult> AddWineToLikes([FromBody] WineToLikesRequest request)
        {
            // print both Ids
            Console.WriteLine($"Account ID: {request.accountId}, Wine ID: {request.wineId}");

            var account = await _context.Account.FindAsync(request.accountId);
            if (account is null)
                return NotFound();
            if (account.LikedWines is null)
                account.LikedWines = new List<Guid>();
            if (!account.LikedWines.Contains(request.wineId))
            {
                account.LikedWines.Add(request.wineId);
                await _context.SaveChangesAsync();
            }
            return NoContent();
        }

        // remove liked wine from account
        [HttpPost("removeWineFromLikes")]
        public async Task<IActionResult> RemoveWineFromLikes([FromBody] WineToLikesRequest request)
        {
            var account = await _context.Account.FindAsync(request.accountId);
            if (account is null)
                return NotFound();
            if (account.LikedWines is null || !account.LikedWines.Contains(request.wineId))
                return BadRequest("Wine not found in liked wines");
            account.LikedWines.Remove(request.wineId);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // get all liked wines for an account
        [HttpGet("likedWines/{accountId}")]
        public async Task<ActionResult<List<Guid>>> GetLikedWines(Guid accountId)
        {
            var account = await _context.Account.FindAsync(accountId);
            if (account is null)
                return NotFound();
            if (account.LikedWines is null)
                return Ok(new List<Guid>());
            return Ok(account.LikedWines);
        }

        // function to hash a string password
        private string hashPassword(string password)
        {
            // initialize SHA256 and string encoding 
            Encoding enc = Encoding.UTF8;
            SHA256 sha256Hash = SHA256.Create();

            // encrypt password as byte array
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