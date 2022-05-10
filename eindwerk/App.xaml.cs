using eindwerk.Services;
using eindwerk.Stores;
using eindwerk.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace eindwerk
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore _NavigationStore;

        public App()
        {
            _NavigationStore = new NavigationStore();
        }
    
        protected override void OnStartup(StartupEventArgs e)
        {
            INavigationService<LoginViewModel> _LoginNavigationService = CreateLoginNavigationService();

            _LoginNavigationService.Navigate();
           

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_NavigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
        private SideBarModel CreateSideBarModel()
        {
           return new SideBarModel(CreatehomeNavigationService(), CreateLearnNavigationService(), CreateAccountNavigationService(), CreateLoginNavigationService());
        }
        private INavigationService<LoginViewModel> CreateLoginNavigationService()
        {
            return new NavigationService<LoginViewModel>(_NavigationStore, () => new LoginViewModel(CreatehomeNavigationService()));
        }

        private INavigationService<AccountPageModel> CreateAccountNavigationService()
        {
            return new LayoutNavigationService<AccountPageModel>(_NavigationStore, () => new AccountPageModel(_NavigationStore), CreateSideBarModel);
        }

        private INavigationService<LearnPageModel> CreateLearnNavigationService()
        {
            return new LayoutNavigationService<LearnPageModel>(_NavigationStore, () => new LearnPageModel(_NavigationStore), CreateSideBarModel);
        }

        private INavigationService<HomePageModel> CreatehomeNavigationService()
        {
            return new LayoutNavigationService<HomePageModel>(_NavigationStore, () => new HomePageModel(_NavigationStore), CreateSideBarModel);
        }

    }
}
