using eindwerk.Stores;
using eindwerk.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace eindwerk.Services
{
    public class NavigationService<TviewModel> : INavigationService<TviewModel> where TviewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<TviewModel> _CreateViewModel;

        public NavigationService(NavigationStore navigationStore, Func<TviewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _CreateViewModel = createViewModel;
        }

        public void Navigate()
        {
            _navigationStore.CurrentViewModel = _CreateViewModel();
        }
    }
}
