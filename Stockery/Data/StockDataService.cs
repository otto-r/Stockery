using Stockery.Model;
using System;
using System.Collections.Generic;

namespace Stockery
{
    public class StockDataService : IStockDataService
    {
        public IEnumerable<Stock> GetStocks()
        {
            yield return new Stock { Id = 1, Name = "Microsoft", Ticker = "MSFT", HistoricalPrices = RandomHistoricalPrice(100) };
            yield return new Stock { Id = 2, Name = "Apple", Ticker = "AAPL", HistoricalPrices = RandomHistoricalPrice(200) };
            yield return new Stock { Id = 3, Name = "Netflix", Ticker = "NFLX", HistoricalPrices = RandomHistoricalPrice(100) };
        }

        public List<double> RandomHistoricalPrice(int basePrice)
        {
            var rnd = new Random();
            List<double> fakeHistoricalPrices = new List<double>();
            for (int i = 0; i < 100; i++)
            {
                var fakePrice = basePrice * rnd.Next(9, 115) / 100;
                fakeHistoricalPrices.Add(fakePrice);
            }
            return fakeHistoricalPrices;
        }
    }
}
