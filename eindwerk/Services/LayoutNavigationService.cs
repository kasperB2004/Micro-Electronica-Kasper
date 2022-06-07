using eindwerk.Stores;
using eindwerk.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eindwerk.Services
{
    public class LayoutNavigationService<TviewModel> : INavigationService where TviewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<SideBarModel> _sidebarModel;
        private readonly Func<TviewModel> _CreateViewModel;

        public LayoutNavigationService(NavigationStore navigationStore,Func<TviewModel> createViewModel, Func<SideBarModel> sidebarModel)
        {
            _navigationStore = navigationStore;
            _sidebarModel = sidebarModel;
            _CreateViewModel = createViewModel;
        }

        public void Navigate()
        {
            _navigationStore.CurrentViewModel = new LayoutViewModel(_sidebarModel() , _CreateViewModel());
        }
    }
}
