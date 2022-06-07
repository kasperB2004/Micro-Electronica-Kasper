using eindwerk.DB;
using eindwerk.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eindwerk.Models;
using eindwerk.Encryption;
using System.Windows;
using eindwerk.Services;
using System.Text.RegularExpressions;

namespace eindwerk.Commands
{
    public class AddAccountCommand : CommandBase
    {
        public readonly AddAccountViewModel _viewModal;
        public readonly INavigationService _navigationService;

        public AddAccountCommand(AddAccountViewModel viewModal, INavigationService navigationService)
        {
            _viewModal = viewModal;
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            if(_viewModal.Name == "" || _viewModal.Email == "" || _viewModal.ChosenClass == null || _viewModal.Chosenpermission == null)
            {
                MessageBox.Show("Wrong inputs, please check if everything is filled in correctly", "Wrong Inputs", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if(!regex.IsMatch(_viewModal.Email))
            {
                MessageBox.Show("Non Valid Email has been used please check if the email has the corect format", "Wrong Inputs", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (var DB = new Database())
            {
                Account account = DB.Accounts.AsQueryable().Where(e => e.Email == _viewModal.Email).FirstOrDefault();
                if(account != null)
                {
                    MessageBox.Show("Email already in the sytem please check if its correct or use another one", "Wrong Inputs", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }


                Class? chosenClass = DB.Class.AsQueryable().Where(i => i.Id == _viewModal.ChosenClass.Id).FirstOrDefault();
                Permission? Chosenpermission = DB.Permissions.AsQueryable().Where(i => i.Id == _viewModal.Chosenpermission.Id).FirstOrDefault();
                var Account = new Account()
                {
                    UserName = _viewModal.Name,
                    Email = _viewModal.Email,
                    Class = chosenClass,
                    Permission = Chosenpermission,
                    Password = Hashing.hash("1")
                };
                DB.Add(Account);
                DB.SaveChanges();
            }
            _navigationService.Navigate();
        }
    }
}
