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
        private readonly ModalNavigationStore _ModalNavigationStore;
        private readonly AccountStore _AccountStore;
        public App()
        {
            _ModalNavigationStore = new ModalNavigationStore();
            _NavigationStore = new NavigationStore();
            _AccountStore = new AccountStore();
            using(var db = new Database())
            {

                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                Class Class2 = new Class()
                {
                    ClassName = "NONSTUDENT"
                };
                var admin = new Permission(){
                    Name = "admin",
                    ViewAccountManagent = true,
                    ViewContentManagent = true,
                };
                var Teacher = new Permission(){
                    Name = "teacher",
                    ViewAccountManagent = false,
                    ViewContentManagent = true,
                }; var student = new Permission(){
                    Name = "student",
                    ViewAccountManagent = false,
                    ViewContentManagent = false,
                };
                db.AddRange(new Account
                {
                    UserName = "Admin",
                    Email = "admin",     
                    Permission = admin,
                    Class = Class2,
                    Password = Hashing.hash("1")

                });
                db.AddRange(new Account
                {
                    UserName = "Langedock",
                    Email = "langedock@guldensporencollege.be",
                    Permission = Teacher,
                    Class = Class2,
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
                    Permission = student,
                    Password = Hashing.hash("1")

                });
                db.SaveChanges();
            }
        }
    
        protected override void OnStartup(StartupEventArgs e)
        {
            INavigationService _LoginNavigationService = CreateLoginNavigationService();

            _LoginNavigationService.Navigate();
           

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_NavigationStore,_ModalNavigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
        private SideBarModel CreateSideBarModel()
        {
           return new SideBarModel(_AccountStore,CreatehomeNavigationService(), CreateLearnNavigationService(), CreateAccountNavigationService(), CreateLoginNavigationService(), CreateAccountManagementNavigationService());
        }
        private INavigationService CreateLoginNavigationService()
        {
            return new NavigationService<LoginViewModel>(_NavigationStore, () => new LoginViewModel(_AccountStore,CreatehomeNavigationService()));
        }

        private INavigationService CreateAccountNavigationService()
        {
            return new LayoutNavigationService<AccountPageModel>(_NavigationStore, () => new AccountPageModel(_NavigationStore), CreateSideBarModel);
        }

        private INavigationService CreateLearnNavigationService()
        {
            return new LayoutNavigationService<LearnPageModel>(_NavigationStore, () => new LearnPageModel(_NavigationStore), CreateSideBarModel);
        }

        private INavigationService CreatehomeNavigationService()
        {
            return new LayoutNavigationService<HomePageModel>(_NavigationStore, () => new HomePageModel(_NavigationStore), CreateSideBarModel);
        }
        public INavigationService CreateCloseModalNavigationService()
        {
            return new CloseModalNavigationService(_ModalNavigationStore);
        }
        public INavigationService CreateAddAccountNavigationService()
        {
            return new ModalNavigationService<AddAccountViewModel>(_ModalNavigationStore, () => new AddAccountViewModel(CreateCloseModalNavigationService(),new CompositeNavigationService(CreateCloseModalNavigationService(), CreateAccountManagementNavigationService())));
        }

        private INavigationService CreateAccountManagementNavigationService()
        {
            return new LayoutNavigationService<AccountManagementViewModel>(_NavigationStore, () => new AccountManagementViewModel(_NavigationStore,CreateAddAccountNavigationService()), CreateSideBarModel);
        }

    }
}
