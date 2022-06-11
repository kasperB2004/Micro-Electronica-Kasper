using eindwerk.Commands;
using eindwerk.DB;
using eindwerk.Models;
using eindwerk.Services;
using eindwerk.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace eindwerk.ViewModels
{
    public class AddAccountViewModel : ViewModelBase
    {

        public string Name { get; set; }
        public string Email { get; set; }
        public Permission Chosenpermission { get; set; }
        public Class ChosenClass { get; set; }
        public List<Permission> PermissionsList { get; set;}
        public List<Class> ClassList { get; set; }
        public ICommand close { get; set; }
        public ICommand saveNewAccount { get; set; }


        public AddAccountViewModel(AccountListStore _AccountListStore, INavigationService CloseModalNavigationService)
        {
            close = new NavigateCommand(CloseModalNavigationService);
            saveNewAccount = new AddAccountCommand(this, CloseModalNavigationService , _AccountListStore);
            using (var Db = new Database())
            {
                PermissionsList = Db.Permissions.ToList();
                ClassList = Db.Class.ToList();
            }
        }
    }
}
