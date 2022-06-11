using eindwerk.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eindwerk.Stores
{
    public class AccountListStore
    {
        public ObservableCollection<Account> Accounts { get; set; }
    }
}
