using eindwerk.Stores;
using eindwerk.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eindwerk.Services
{
    public class ModalNavigationService<TviewModel> : INavigationService where TviewModel : ViewModelBase
    {
        private readonly ModalNavigationStore _navigationStore;
        private readonly Func<TviewModel> _createViewModel;

        public ModalNavigationService(ModalNavigationStore navigationStore, Func<TviewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate()
        {
          _navigationStore.CurrentViewModel = _createViewModel();
        }
    }
}
