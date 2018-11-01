using Stockery.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Stockery.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        public MainViewModel(INavigationViewModel navigationViewModel)
        {
            NavigationViewModel = navigationViewModel;
        }

        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }
        public INavigationViewModel NavigationViewModel { get; }
    }
}
