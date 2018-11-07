using Prism.Events;
using Stockery.Data;
using Stockery.Data.LookUps;
using Stockery.Event;
using System;
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
            _eventAggregator.GetEvent<AfterStockDeletedEvent>().Subscribe(AfterStockDeleted);
        }

        public async Task LoadAsync()
        {
            var lookup = await _stockLookUpDataService.GetStockLookUpAsync();
            Stocks.Clear();
            foreach (var item in lookup)
            {
                Stocks.Add(new NavigationItemViewModel(item.Id, item.DisplayMember, _eventAggregator));
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

        private void AfterStockDeleted(int stockId)
        {
            var stock = Stocks.SingleOrDefault(s => s.Id == stockId);
            if (stock!=null)
            {
                Stocks.Remove(stock);
            }
        }

        private void AfterStockSaved(AfterStockSavedEventArgs obj)
        {
            var lookUpItem = Stocks.SingleOrDefault(s => s.Id == obj.Id);
            if (lookUpItem == null)
            {
                Stocks.Add(new NavigationItemViewModel(obj.Id, obj.DisplayMember, _eventAggregator));
            }
            else
            {
                lookUpItem.DisplayMember = obj.DisplayMember;
            }
        }
    }
}
