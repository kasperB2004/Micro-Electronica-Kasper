using eindwerk.DB;
using eindwerk.Models;
using eindwerk.Stores;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eindwerk.ViewModels
{
    public  class AccountManagementViewModel : ViewModelBase
    {
        public List<Account> Accounts { get; set; }
        public Account SelectedItem { get; set; }
        public AccountManagementViewModel(NavigationStore navigationStore)
        {
            Accounts = new List<Account>();
            using(var db = new Database())
            {
                Accounts = db.Accounts.Include(c => c.Class).ToList();
            }
        }
    }
}
