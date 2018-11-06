﻿using Prism.Commands;
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
        private IMessageDialogService _messageDialogService;
        private IStockDetailViewModel _stockDetailViewModel;

        public MainViewModel(INavigationViewModel navigationViewModel,
            Func<IStockDetailViewModel> stockDetailViewModelCreator,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
        {
            _eventAggregator = eventAggregator;
            _stockDetailViewModelCreator = stockDetailViewModelCreator;
            _messageDialogService = messageDialogService;

            _eventAggregator.GetEvent<OpenStockDetailViewEvent>().Subscribe(OnOpenStockDetailView);

            AddNewStockCommand = new DelegateCommand(OnAddNewStockExecute);

            NavigationViewModel = navigationViewModel;
        }


        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }

        public ICommand AddNewStockCommand { get; }

        public INavigationViewModel NavigationViewModel { get; }

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

    }
}
