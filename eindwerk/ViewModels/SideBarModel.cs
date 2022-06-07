using eindwerk.Commands;
using eindwerk.Services;
using eindwerk.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace eindwerk.ViewModels
{
    public class SideBarModel : ViewModelBase
    {
        private readonly AccountStore _accountStore;
        public bool ViewContentManagent => _accountStore.ViewContentManagent;

        public bool ViewAccountManagent => _accountStore.ViewAccountManagent;
        public ICommand NavigateHomeCommand{get;}
        public ICommand NavigateLearnCommand { get; }
        public ICommand NavigateAccountCommand { get; }
        public ICommand NavigateLogOutCommand { get; }
        public ICommand CloseCommand { get; }

        public ICommand NavigateAccountManagementCommand { get; }


        public SideBarModel(AccountStore AccountStore, INavigationService homeNavigationService, INavigationService LearnNavigationService,INavigationService AccountNavigationService, INavigationService LoginNavigationService, INavigationService AccountManagementnavigationService)
        {
            _accountStore = AccountStore;
            NavigateHomeCommand = new NavigateCommand(homeNavigationService);
            NavigateLearnCommand = new NavigateCommand(LearnNavigationService);
            NavigateAccountCommand = new NavigateCommand(AccountNavigationService);
            NavigateLogOutCommand = new NavigateCommand(LoginNavigationService);
            NavigateAccountManagementCommand = new NavigateCommand(AccountManagementnavigationService);
           CloseCommand = new CloseCommand();
        }
    }
}
    