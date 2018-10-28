using System.Collections.Generic;
using Stockery.Model;

namespace Stockery
{
    public interface IStockDataService
    {
        IEnumerable<Stock> GetStocks();
        List<HistoricalStockPriceInfo> RandomHistoricalPriceInfo(int basePrice);
    }
}