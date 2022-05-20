using eindwerk.Models;
using eindwerk.Services;
using eindwerk.Stores;
using eindwerk.ViewModels;
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
        private readonly INavigationService<HomePageModel> _navigationService;
        private AccountStore _AccountStore;

        public LoginCommand(LoginViewModel viewModel, INavigationService<HomePageModel> navigationService, AccountStore accountStore)
        { 
            _viewModel = viewModel;
            _AccountStore = accountStore;
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            //logic for logging in can go hoere
            Account account = new Account(){
                UserName = _viewModel.Username,
                Password = _viewModel.Password,
                Email = $"{_viewModel.Username}@test.com",
                IsTeacher = _viewModel.Username == "greg" ? true : false
                
            };

            _AccountStore.CurrentAccount = account;
         
            MessageBox.Show($"logging user in with username {_viewModel.Username} and with password {_viewModel.Password} ");

            _navigationService.Navigate();

        }
    }
}
