using eindwerk.DB;
using eindwerk.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace eindwerk.Commands
{
    public class RemoveAccountCommand : CommandBase
    {
        public AccountManagementViewModel AccountManagementViewModel { get; }
        public RemoveAccountCommand(AccountManagementViewModel accountManagementViewModel)
        {
            AccountManagementViewModel = accountManagementViewModel;
        }


        public override void Execute(object parameter)
        {
            if(AccountManagementViewModel.SelectedItem == null)
            {
                MessageBox.Show("nothing has been selected, please select a account and try again", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            using(var db = new Database())
            {
                db.Remove(AccountManagementViewModel.SelectedItem);
                db.SaveChanges();
                AccountManagementViewModel._AccountList.Accounts.Remove(AccountManagementViewModel.SelectedItem);
            }
        }
    }
}
