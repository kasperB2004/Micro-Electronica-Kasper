using Microsoft.EntityFrameworkCore;
using EindwerkApi.Models;

namespace EindwerkApi.Database
{
    public class EindwerkApiDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        public EindwerkApiDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
