using Prism.Commands;
using Prism.Events;
using Stockery.Event;
using Stockery.View.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Stockery.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        private IEventAggregator _eventAggregator;
        private Func<IStockDetailViewModel> _stockDetailViewModelCreator;
        private Func<IBondDetailViewModel> _bondDetailViewModelCreator;
        private IMessageDialogService _messageDialogService;
        private IStockDetailViewModel _stockDetailViewModel;
        private IBondDetailViewModel _bondDetailViewModel;

        public MainViewModel(INavigationViewModel navigationViewModel,
            INavigationBondViewModel navigationBondViewModel,
            Func<IStockDetailViewModel> stockDetailViewModelCreator,
            Func<IBondDetailViewModel> bondDetailViewModelCreator,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
        {
            _eventAggregator = eventAggregator;
            _stockDetailViewModelCreator = stockDetailViewModelCreator;
            _bondDetailViewModelCreator = bondDetailViewModelCreator;
            _messageDialogService = messageDialogService;

            _eventAggregator.GetEvent<OpenStockDetailViewEvent>().Subscribe(OnOpenStockDetailView);
            _eventAggregator.GetEvent<OpenBondDetailViewEvent>().Subscribe(OnOpenBondDetailView);

            AddNewStockCommand = new DelegateCommand(OnAddNewStockExecute);
            AddNewBondCommand = new DelegateCommand(OnAddNewBondExecute);

            NavigationViewModel = navigationViewModel;
            NavigationBondViewModel = navigationBondViewModel;
        }


        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
            await NavigationBondViewModel.LoadAsync();
        }

        public ICommand AddNewStockCommand { get; }
        public ICommand AddNewBondCommand { get; }

        public INavigationViewModel NavigationViewModel { get; }
        public INavigationBondViewModel NavigationBondViewModel { get; }

        public IStockDetailViewModel StockDetailViewModel
        {
            get { return _stockDetailViewModel; }
            private set
            {
                _stockDetailViewModel = value;
                OnPropertyChanged();
            }
        }

        private async void OnOpenStockDetailView(int? stockId)
        {
            if (StockDetailViewModel != null && StockDetailViewModel.HasChanges)
            {
                var result = _messageDialogService.ShowOkCancelDialog("You've made changes. Navigate away", "Question");
                if (result == MessageDialogResult.Cancel)
                {
                    return;
                }
            }
            StockDetailViewModel = _stockDetailViewModelCreator();
            await StockDetailViewModel.LoadAsync(stockId);
        }

        private void OnAddNewStockExecute()
        {
            OnOpenStockDetailView(null);
        }

        public IBondDetailViewModel BondDetailViewModel
        {
            get { return _bondDetailViewModel; }
            private set
            {
                _bondDetailViewModel = value;
                OnPropertyChanged();
            }
        }

        private async void OnOpenBondDetailView(int? bondId)
        {
            if (BondDetailViewModel != null && BondDetailViewModel.HasChanges)
            {
                var result = _messageDialogService.ShowOkCancelDialog("You've made changes. Navigate away", "Question");
                if (result == MessageDialogResult.Cancel)
                {
                    return;
                }
            }
            BondDetailViewModel = _bondDetailViewModelCreator();
            await BondDetailViewModel.LoadAsync(bondId);
        }

        private void OnAddNewBondExecute()
        {
            OnOpenBondDetailView(null);
        }
    }
}
