using Prism.Commands;
using Prism.Events;
using Stockery.Event;
using Stockery.Model;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Stockery.ViewModel
{
    public class StockDetailViewModel : ViewModelBase, IStockDetailViewModel
    {
        private IStockDataService _stockDataService;
        private IEventAggregator _eventAggregator;

        public StockDetailViewModel(IStockDataService stockDataService, IEventAggregator eventAggregator)
        {
            _stockDataService = stockDataService;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<OpenStockDetailViewEvent>().Subscribe(OnOpenStockDetailView);

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
        }

        private async void OnSaveExecute()
        {
            await _stockDataService.SaveAsync(Stock);
            _eventAggregator.GetEvent<AfterStockSavedEvent>().Publish(new AfterStockSavedEventArgs
            {
                Id = Stock.Id,
                DisplayMember = $"{Stock.Name} - {Stock.Ticker}"
            });
        }

        private bool OnSaveCanExecute()
        {
            //todo
            return true;
        }

        private async void OnOpenStockDetailView(int stockId)
        {
            await LoadAsync(stockId);
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

        public ICommand SaveCommand { get; }
    }
}
