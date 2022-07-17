using EindwerkApi.Database;
using EindwerkApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using EindwerkApi.Encryption;

namespace EindwerkApi.Controllers
{
    [ApiController]
    [RequireHttps]
    [Route("api/[controller]")]
    public class AccountsControler : ControllerBase
    {
        private readonly EindwerkApiDbContext DbContext;
        public AccountsControler(EindwerkApiDbContext dbContext)
        {
            DbContext = dbContext;
        }


        [HttpGet("GetAccountList")]
        public async Task<IEnumerable<Account>> Get()
        {
           //return List of all accounts
           return await DbContext.Accounts.ToListAsync();
        }
        [HttpGet("GetAccount/")]
        public async Task<IActionResult> Get(int? id, string? username)
        {
            if (id == null && username == null)
                return BadRequest();
            Account? acc = await DbContext.Accounts.AsQueryable().Where(i => i.Id == id || i.UserName == username).FirstOrDefaultAsync();
            if (acc == null)
                return NoContent();
            return Ok(acc);
        }


        [HttpPost("AddAccount")]
        public async Task<IActionResult> Post(NewAccountRequest NewAccountRequest)
        {

            Account? CheckAccount = await DbContext.Accounts.AsQueryable().Where(i => i.Email == NewAccountRequest.Email || i.UserName == NewAccountRequest.UserName).FirstOrDefaultAsync();

            if(!(CheckAccount == null))
            {
                return BadRequest();
            }
            
            Account account = new Account()
            {
                Email = NewAccountRequest.Email,
                FullName = NewAccountRequest.FullName,
                UserName = NewAccountRequest.UserName,
                Password = Hashing.hash(Hashing.PasswordGenerator())
            };
            await DbContext.Accounts.AddAsync(account);
            await DbContext.SaveChangesAsync();
           return Ok();
        }

        [HttpDelete("DeleteAccount")]
        public async Task<IActionResult> Delete(int id)
        {
            Account? acc = await DbContext.Accounts.AsQueryable().Where(i =>i.Id == id).FirstOrDefaultAsync();
            if(acc == null)
                return NoContent();            
            DbContext.Accounts.Remove(acc);
            await DbContext.SaveChangesAsync();
            return Ok();
        }   


    }
}