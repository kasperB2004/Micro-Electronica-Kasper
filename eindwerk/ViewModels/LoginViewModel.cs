using eindwerk.Commands;
using eindwerk.Services;
using eindwerk.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace eindwerk.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {  
        public ICommand LogInCommand { get; }
        public ICommand CloseCommand { get; }

        private string _username;
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private String _password;
        public String Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }


        public LoginViewModel(AccountStore AccountStore, INavigationService _HomepageModel)
        {                
            LogInCommand = new LoginCommand(this, _HomepageModel,AccountStore);
            CloseCommand = new CloseCommand();
        }
    }
}
