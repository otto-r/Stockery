using System.Collections.Generic;
using Stockery.Model;

namespace Stockery
{
    public interface IStockDataService
    {
        IEnumerable<Stock> GetStocks();
        List<double> RandomHistoricalPrice(int basePrice);
    }
}