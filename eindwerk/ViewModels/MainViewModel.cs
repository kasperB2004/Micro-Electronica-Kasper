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
        private readonly ModalNavigationStore _ModalNavigationStore;

        public ViewModelBase CurrentViewModel => _NavigationStore.CurrentViewModel;
        public ViewModelBase CurrentModalViewModel => _ModalNavigationStore.CurrentViewModel;
        public bool IsModalOpen => _ModalNavigationStore.IsOpen;
        public MainViewModel(NavigationStore navigationStore, ModalNavigationStore modalNavigationStore)
        {
            _NavigationStore = navigationStore;
            _ModalNavigationStore = modalNavigationStore;
            _NavigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            _ModalNavigationStore.CurrentViewModelChanged += OnCurrentModalViewModelChanged;
        }

        private void OnCurrentModalViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentModalViewModel));
            OnPropertyChanged(nameof(IsModalOpen));
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));

        }
    }
}
