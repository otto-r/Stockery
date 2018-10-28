namespace Stockery.DataAccess.Migrations
{
    using Stockery.Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Stockery.DataAccess.StockDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Stockery.DataAccess.StockDbContext context)
        {
            context.Stocks.AddOrUpdate(
                s => s.Name,
                new Stock
                {
                    Id = 1,
                    Name = "Microsoft",
                    Ticker = "MSFT",
                    HistoricalPrices = new List<HistoricalStockPriceInfo>
                    {
                        new HistoricalStockPriceInfo {Id = 1, Price = 100},
                        new HistoricalStockPriceInfo {Id = 2, Price = 99},
                        new HistoricalStockPriceInfo {Id = 3, Price = 101}
                    }
                },
                new Stock
                {
                    Id = 2,
                    Name = "Apple",
                    Ticker = "AAPL",
                    HistoricalPrices = new List<HistoricalStockPriceInfo>
                    {
                        new HistoricalStockPriceInfo {Id = 1, Price = 45},
                        new HistoricalStockPriceInfo {Id = 2, Price = 46},
                        new HistoricalStockPriceInfo {Id = 3, Price = 47}
                    }
                },
                new Stock
                {
                    Id = 3,
                    Name = "Netflix",
                    Ticker = "NFLX",
                },
                new Stock
                {
                    Id = 4,
                    Name = "Alphabet",
                    Ticker = "GOOG",
                }
                );
        }

        //public List<HistoricalStockPriceInfo> RandomHistoricalPriceInfo(int basePrice)
        //{
        //    var date = DateTime.Now;
        //    var rnd = new Random();
        //    var fakeHistoricalPrices = new List<HistoricalStockPriceInfo>();

        //    for (int i = 0; i < 100; i++)
        //    {
        //        var fakeHistoricalStockPriceInfo = new HistoricalStockPriceInfo
        //        {
        //            Price = basePrice * rnd.Next(9, 115) / 100
        //        };
        //        fakeHistoricalStockPriceInfo.Date = date.AddDays(-1);

        //        fakeHistoricalPrices.Add(fakeHistoricalStockPriceInfo);
        //    }

        //    return fakeHistoricalPrices;
        //}
    }
}
