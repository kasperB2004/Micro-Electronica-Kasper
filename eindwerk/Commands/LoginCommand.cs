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
        private readonly LoginViewModel _loginViewModel;
        private readonly INavigationService<HomePageModel> _navigationService;

        public LoginCommand(LoginViewModel loginViewModel, INavigationService<HomePageModel> navigationService)
        {
            _loginViewModel = loginViewModel;
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            //logic for logging in can go hoere
           
            MessageBox.Show($"logging user in with username {_loginViewModel.Username} and with password {_loginViewModel.Password} ");

            _navigationService.Navigate();

        }
    }
}
