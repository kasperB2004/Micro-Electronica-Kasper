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
        public bool IsTeacher => _accountStore.IsTeacher;
        public ICommand NavigateHomeCommand{get;}
        public ICommand NavigateLearnCommand { get; }
        public ICommand NavigateAccountCommand { get; }
        public ICommand NavigateLogOutCommand { get; }
        public ICommand CloseCommand { get; }


        public SideBarModel(AccountStore AccountStore, INavigationService<HomePageModel> homeNavigationService, INavigationService<LearnPageModel> LearnNavigationService,INavigationService<AccountPageModel> AccountNavigationService, INavigationService<LoginViewModel> LoginNavigationService)
        {
            _accountStore = AccountStore;
            NavigateHomeCommand = new NavigateCommand<HomePageModel>(homeNavigationService);
            NavigateLearnCommand = new NavigateCommand<LearnPageModel>(LearnNavigationService);
            NavigateAccountCommand = new NavigateCommand<AccountPageModel>(AccountNavigationService);
            NavigateLogOutCommand = new NavigateCommand<LoginViewModel>(LoginNavigationService);
           CloseCommand = new CloseCommand();
        }
    }
}
    