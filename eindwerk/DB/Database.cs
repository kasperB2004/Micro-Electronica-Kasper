using eindwerk.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eindwerk.DB
{

    public partial class Database : DbContext
    {

        //specifiying tables
        public virtual DbSet<Account> Accounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(@"data source=localhost\MSSQLSERVER01;initial catalog=MicroElectronica;trusted_connection=true");
        }

    }
}
