using eindwerk.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eindwerk.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _NavigationStore;  
        public ViewModelBase CurrentViewModel => _NavigationStore.CurrentViewModel;

        public MainViewModel(NavigationStore navigationStore)
        {
            _NavigationStore = navigationStore;

            _NavigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
      
        }
    }
}
