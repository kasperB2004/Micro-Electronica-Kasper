using eindwerk.ViewModels;

namespace eindwerk.Services
{
    public interface INavigationService<TviewModel> where TviewModel : ViewModelBase
    {
        void Navigate();
    }
}