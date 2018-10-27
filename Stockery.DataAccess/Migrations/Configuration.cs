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
                new Stock { Id = 1, Name = "Microsoft", Ticker = "MSFT" },
                new Stock { Id = 2, Name = "Apple", Ticker = "AAPL" },
                new Stock { Id = 3, Name = "Netflix", Ticker = "NFLX" }
                );
        }

        //public List<double> RandomHistoricalPrice(int basePrice)
        //{
        //    var rnd = new Random();
        //    List<double> fakeHistoricalPrices = new List<double>();
        //    for (int i = 0; i < 100; i++)
        //    {
        //        var fakePrice = basePrice * rnd.Next(9, 115) / 100;
        //        fakeHistoricalPrices.Add(fakePrice);
        //    }
        //    return fakeHistoricalPrices;
        //}
    }
}
