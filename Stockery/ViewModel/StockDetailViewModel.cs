using Prism.Commands;
using Prism.Events;
using Stockery.Data.Repositories;
using Stockery.Event;
using Stockery.Model;
using Stockery.Wrapper;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Stockery.ViewModel
{
    public class StockDetailViewModel : ViewModelBase, IStockDetailViewModel
    {
        private IStockRepository _stockRepository;
        private IEventAggregator _eventAggregator;
        private StockWrapper _stock;
        private bool _hasChanges;


        public StockDetailViewModel(IStockRepository stockRepository, IEventAggregator eventAggregator)
        {
            _stockRepository = stockRepository;
            _eventAggregator = eventAggregator;

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute);
        }

        private async void OnDeleteExecute()
        {
            _stockRepository.Remove(Stock.Model);
            await _stockRepository.SaveAsync();
            _eventAggregator.GetEvent<AfterStockDeletedEvent>().Publish(Stock.Id);
        }

        public async Task LoadAsync(int? stockId)
        {
            var stock = stockId.HasValue
                ? await _stockRepository.GetByIdAsync(stockId.Value)
            : AddNewStock();
            Stock = new StockWrapper(stock);
            Stock.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _stockRepository.HasChanges();
                }
                if (e.PropertyName == nameof(Stock.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };
            if (Stock.Id == 0)
            {
                Stock.Name = "";
            }
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

        public bool HasChanges
        {
            get { return _hasChanges; }
            set
            {
                if (_hasChanges != value)
                {
                    _hasChanges = value;
                    OnPropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }

        private async void OnSaveExecute()
        {
            await _stockRepository.SaveAsync();
            HasChanges = _stockRepository.HasChanges();
            _eventAggregator.GetEvent<AfterStockSavedEvent>().Publish(new AfterStockSavedEventArgs
            {
                Id = Stock.Id,
                DisplayMember = $"{Stock.Name} - {Stock.Ticker}"
            });
        }

        private bool OnSaveCanExecute()
        {
            return Stock != null && !Stock.HasErrors && HasChanges;
        }

        private Stock AddNewStock()
        {
            var stock = new Stock();
            _stockRepository.Add(stock);
            return stock;
        }
    }
}
