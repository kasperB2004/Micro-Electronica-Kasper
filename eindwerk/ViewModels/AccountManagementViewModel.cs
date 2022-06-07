using eindwerk.Commands;
using eindwerk.DB;
using eindwerk.Models;
using eindwerk.Services;
using eindwerk.Stores;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace eindwerk.ViewModels
{
    public  class AccountManagementViewModel : ViewModelBase
    {
        public List<Account> Accounts { get; set; }
        public Account SelectedItem { get; set; }

        public ICommand NavigateAddAccountCommand { get;}

        public AccountManagementViewModel(NavigationStore navigationStore, INavigationService _NavigateAddAccount)
        {
            NavigateAddAccountCommand = new NavigateCommand(_NavigateAddAccount);
            Accounts = new List<Account>();
            using(var db = new Database())
            {
                Accounts = db.Accounts.Include(c => c.Class).Include(p => p.Permission).ToList();
            }
        }
    }
}
