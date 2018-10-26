using Stockery.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Stockery.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private IStockDataService _stockDataService;
        private Stock _selectedStock;

        public MainViewModel(IStockDataService stockDataService)
        {
            Stocks = new ObservableCollection<Stock>();
            _stockDataService = stockDataService;
        }

        public void Load()
        {
            var stocks = _stockDataService.GetStocks();
            Stocks.Clear();
            foreach (var stock in stocks)
            {
                Stocks.Add(stock);
            }
        }

        public ObservableCollection<Stock> Stocks { get; set; }

        public Stock SelectedStock
        {
            get { return _selectedStock; }
            set
            {
                _selectedStock = value;
                OnPropertyChanged();
            }
        }
    }
}
