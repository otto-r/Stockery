using Stockery.Model;
using System.Threading.Tasks;

namespace Stockery.ViewModel
{
    public class StockDetailViewModel : ViewModelBase, IStockDetailViewModel
    {
        private IStockDataService _stockDataService;

        public StockDetailViewModel(IStockDataService stockDataService)
        {
            _stockDataService = stockDataService;
        }

        public async Task LoadAsync(int stockId)
        {
            Stock = await _stockDataService.GetByIdAsync(stockId);
        }

        private Stock _stock;

        public Stock Stock
        {
            get { return _stock; }
            private set
            {
                _stock = value;
                OnPropertyChanged();
            }
        }
    }
}
