using eindwerk.DB;
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
using eindwerk.Models;
using eindwerk.Encryption;

namespace eindwerk
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore _NavigationStore;
        private readonly AccountStore _AccountStore;
        public App()
        {
            _NavigationStore = new NavigationStore();
            _AccountStore = new AccountStore();
            using(var db = new Database())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                db.Add(new Account
                {
                    UserName = "Admin",
                    Email = "admin@guldensporencollege.be",
                    AccountTypeId = 2,
                    Password = Hashing.hash("1")

                });
                db.Add(new Account
                {
                    UserName = "Langedock",
                    Email = "langedock@guldensporencollege.be",
                    AccountTypeId = 1,
                    Password = Hashing.hash("1")

                });
                db.SaveChanges();
                Class Class = new Class()
                {
                    ClassName = "5IICT"
                };
                db.AddRange(new Account
                {
                    UserName = "Kasper",
                    Email = "kasper@guldensporencollege.be",
                    Class = Class,
                    AccountTypeId = 0,
                    Password = Hashing.hash("1")

                });
                db.SaveChanges();
            }
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
           return new SideBarModel(_AccountStore,CreatehomeNavigationService(), CreateLearnNavigationService(), CreateAccountNavigationService(), CreateLoginNavigationService(), CreateAccountManagementNavigationService());
        }
        private INavigationService<LoginViewModel> CreateLoginNavigationService()
        {
            return new NavigationService<LoginViewModel>(_NavigationStore, () => new LoginViewModel(_AccountStore,CreatehomeNavigationService()));
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
        private INavigationService<AccountManagementViewModel> CreateAccountManagementNavigationService()
        {
            return new LayoutNavigationService<AccountManagementViewModel>(_NavigationStore, () => new AccountManagementViewModel(_NavigationStore), CreateSideBarModel);
        }

    }
}
