using eindwerk.Stores;
using eindwerk.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eindwerk.Services
{
    public class ParameterNavigationService<TParameter, TviewModel> where TviewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<TParameter,TviewModel> _CreateViewModel;

        public ParameterNavigationService(NavigationStore navigationStore, Func<TParameter, TviewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _CreateViewModel = createViewModel;
        }

        public void Navigate(TParameter parameter)
        {
            _navigationStore.CurrentViewModel = _CreateViewModel(parameter); 
        }
    }
}
