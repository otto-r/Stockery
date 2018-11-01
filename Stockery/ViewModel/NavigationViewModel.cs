using Prism.Events;
using Stockery.Data;
using Stockery.Event;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Stockery.ViewModel
{
    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        private IStockLookUpDataService _stockLookUpDataService;
        private IEventAggregator _eventAggregator;

        public NavigationViewModel(IStockLookUpDataService stockLookUpDataService, IEventAggregator eventAggregator)
        {
            _stockLookUpDataService = stockLookUpDataService;
            _eventAggregator = eventAggregator;
            Stocks = new ObservableCollection<NavigationItemViewModel>();
            _eventAggregator.GetEvent<AfterStockSavedEvent>().Subscribe(AfterStockSaved);
        }

        private void AfterStockSaved(AfterStockSavedEventArgs obj)
        {
            var lookUpItem = Stocks.Single(s => s.Id == obj.Id);
            lookUpItem.DisplayMember = obj.DisplayMember;
        }

        public async Task LoadAsync()
        {
            var lookup = await _stockLookUpDataService.GetStockLookUpAsync();
            Stocks.Clear();
            foreach (var item in lookup)
            {
                Stocks.Add(new NavigationItemViewModel(item.Id, item.DisplayMember));
            }
        }
        public ObservableCollection<NavigationItemViewModel> Stocks { get; }

        private NavigationItemViewModel _selectedStock;

        public NavigationItemViewModel SelectedStock
        {
            get { return _selectedStock; }
            set
            {
                _selectedStock = value;
                OnPropertyChanged();
                if (_selectedStock != null)
                {
                    _eventAggregator.GetEvent<OpenStockDetailViewEvent>().Publish(_selectedStock.Id);
                }
            }
        }

    }
}
