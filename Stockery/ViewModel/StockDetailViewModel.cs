using Prism.Commands;
using Prism.Events;
using Stockery.Event;
using Stockery.Wrapper;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Stockery.ViewModel
{
    public class StockDetailViewModel : ViewModelBase, IStockDetailViewModel
    {
        private IStockDataService _stockDataService;
        private IEventAggregator _eventAggregator;
        private StockWrapper _stock;

        public StockDetailViewModel(IStockDataService stockDataService, IEventAggregator eventAggregator)
        {
            _stockDataService = stockDataService;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<OpenStockDetailViewEvent>().Subscribe(OnOpenStockDetailView);

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
        }

        public async Task LoadAsync(int stockId)
        {
            var stock = await _stockDataService.GetByIdAsync(stockId);
            Stock = new StockWrapper(stock);
            Stock.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(Stock.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        public StockWrapper Stock
        {
            get { return _stock; }
            private set
            {
                _stock = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }

        private async void OnSaveExecute()
        {
            await _stockDataService.SaveAsync(Stock.Model);
            _eventAggregator.GetEvent<AfterStockSavedEvent>().Publish(new AfterStockSavedEventArgs
            {
                Id = Stock.Id,
                DisplayMember = $"{Stock.Name} - {Stock.Ticker}"
            });
        }

        private bool OnSaveCanExecute()
        {
            //todo
            return Stock != null && !Stock.HasErrors;
        }

        private async void OnOpenStockDetailView(int stockId)
        {
            await LoadAsync(stockId);
        }
    }
}
