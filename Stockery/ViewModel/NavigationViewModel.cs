using Stockery.Data;
using Stockery.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stockery.ViewModel
{
    public class NavigationViewModel : INavigationViewModel
    {
        private IStockLookUpDataService _stockLookUpDataService;

        public NavigationViewModel(IStockLookUpDataService stockLookUpDataService)
        {
            _stockLookUpDataService = stockLookUpDataService;
            Stocks = new ObservableCollection<LookUpItem>();
        }

        public async Task LoadAsync()
        {
            var lookup = await _stockLookUpDataService.GetStockLookUpAsync();
            Stocks.Clear();
            foreach (var item in lookup)
            {
                Stocks.Add(item);
            }
        }
        public ObservableCollection<LookUpItem> Stocks { get; }
    }
}
