using eindwerk.DB;
using eindwerk.Encryption;
using eindwerk.Models;
using eindwerk.Services;
using eindwerk.Stores;
using eindwerk.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace eindwerk.Commands
{
    public class LoginCommand : CommandBase
    {
        private readonly LoginViewModel _viewModel;
        private readonly INavigationService _navigationService;
        private AccountStore _AccountStore;

        public LoginCommand(LoginViewModel viewModel, INavigationService navigationService, AccountStore accountStore)
        { 
            _viewModel = viewModel;
            _AccountStore = accountStore;
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {

            if(_viewModel.Username == "" ||_viewModel.Password == "" )
            {
                MessageBox.Show($"Please fill in all input boxes ", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (var db = new Database())
            {
                var account = db.Accounts.AsQueryable().Where(u => u.Email.ToLower() == _viewModel.Username.ToLower()).Include(p => p.Permission).FirstOrDefault();
                if(account == null)
                {
                    MessageBox.Show($"Account not found, try again ","error",MessageBoxButton.OK,MessageBoxImage.Error);

                    return;
                }
                var correctpassword = Hashing.verify(account.Password,_viewModel.Password);
                if (correctpassword)
                {
                    _AccountStore.CurrentAccount = account;
                    _navigationService.Navigate();

                }
                else
                {
                    MessageBox.Show($"Wrong password, try again ", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }
    }
}
