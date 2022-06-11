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
        public AccountListStore _AccountList { get; set; }
        public ObservableCollection<Account> Accounts => _AccountList.Accounts;
        public Account SelectedItem { get; set; }

        public ICommand NavigateAddAccountCommand { get;}

        public AccountManagementViewModel(NavigationStore navigationStore, AccountListStore AccountListStore, INavigationService _NavigateAddAccount)
        {
            _AccountList = AccountListStore;
            NavigateAddAccountCommand = new NavigateCommand(_NavigateAddAccount);
            filldatagrid();
        }
        private async void filldatagrid()
        {
            using(var db = new Database())
            {
                _AccountList.Accounts = new ObservableCollection<Account>(db.Accounts.Include(c => c.Class).Include(p => p.Permission));   
            }

        }
    }
}
