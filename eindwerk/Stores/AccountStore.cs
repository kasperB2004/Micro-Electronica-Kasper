using eindwerk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eindwerk.Stores
{
    public class AccountStore
    {
        private Account _currentAccount;

        private Permission _permission;
        public Account CurrentAccount
        {
            get => _currentAccount;
            set
            {
                _currentAccount = value;
                _permission = value.Permission;
                CurrentAccountChanged?.Invoke();
            }
        }

        public bool ViewContentManagent => _permission.ViewContentManagent;
        public bool ViewAccountManagent => _permission.ViewAccountManagent;

        public event Action CurrentAccountChanged;

        public void Logout()
        {
            CurrentAccount = null;
        }
    }
    
}
