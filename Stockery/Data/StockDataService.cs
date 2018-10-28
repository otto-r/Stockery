using Stockery.Model;
using System;
using System.Collections.Generic;

namespace Stockery
{
    public class StockDataService : IStockDataService
    {
        public IEnumerable<Stock> GetStocks()
        {
            yield return new Stock { Id = 1, Name = "Microsoft", Ticker = "MSFT", HistoricalPrices = RandomHistoricalPriceInfo(100) };
            yield return new Stock { Id = 2, Name = "Apple", Ticker = "AAPL", HistoricalPrices = RandomHistoricalPriceInfo(200) };
            yield return new Stock { Id = 3, Name = "Netflix", Ticker = "NFLX", HistoricalPrices = RandomHistoricalPriceInfo(100) };
        }

        public List<HistoricalStockPriceInfo> RandomHistoricalPriceInfo(int basePrice)
        {
            var date = DateTime.Now;
            var rnd = new Random();
            var fakeHistoricalPrices = new List<HistoricalStockPriceInfo>();

            for (int i = 0; i < 100; i++)
            {
                var fakeHistoricalStockPriceInfo = new HistoricalStockPriceInfo();
                fakeHistoricalStockPriceInfo.Price = basePrice * rnd.Next(9, 115) / 100;
                fakeHistoricalPrices.Add(fakeHistoricalStockPriceInfo);
                //fakeHistoricalStockPriceInfo.Date = date = date.AddDays(-1);

                fakeHistoricalPrices.Add(fakeHistoricalStockPriceInfo);
            }

            return fakeHistoricalPrices;
        }
    }
}
