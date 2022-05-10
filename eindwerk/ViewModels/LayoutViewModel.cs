using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eindwerk.ViewModels
{
    public class LayoutViewModel : ViewModelBase
    {
        public SideBarModel SideBar { get; }
        public ViewModelBase ContentViewModel { get; }
        public LayoutViewModel(SideBarModel sideBar, ViewModelBase contentViewModel)
        {
            SideBar = sideBar;
            ContentViewModel = contentViewModel;
        }

    }
}
  