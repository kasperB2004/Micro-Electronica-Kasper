using EindwerkApi.Database;
using EindwerkApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace EindwerkApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsControler : Controller
    {
        private readonly EindwerkApiDbContext DbContext;
        public AccountsControler(EindwerkApiDbContext dbContext)
        {
            DbContext = dbContext;
        }


        [HttpGet(Name ="GetAccountList")]
        public async Task<IEnumerable<Account>> Get()
        {
           return await DbContext.Accounts.ToListAsync();
        }


        [HttpPost(Name = "AddAccount")]
        public async Task<IActionResult> Post(NewAccountRequest NewAccountRequest)
        {
            return BadRequest("UserName Already used");

            Account account = new Account()
            {
                Email = NewAccountRequest.Email,
                FullName = NewAccountRequest.FullName,
                UserName = NewAccountRequest.UserName,
                Password = NewAccountRequest.Password
            };
            await DbContext.Accounts.AddAsync(account);
            await DbContext.SaveChangesAsync();

           return Ok(account);
        }
    }
}