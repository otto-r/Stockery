namespace Stockery.DataAccess.Migrations
{
    using Stockery.Model;
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
                },
                new Stock
                {
                    Id = 2,
                    Name = "Apple",
                    Ticker = "AAPL",
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
            context.Bonds.AddOrUpdate(b => b.Name,
                new Bond
                {
                    Id = 1,
                    Name = "3 MONTH"
                },
                new Bond
                {
                    Id = 2,
                    Name = "6 MONTH"
                },
                new Bond
                {
                    Id = 3,
                    Name = "12 MONTH"
                },
                new Bond
                {
                    Id = 5,
                    Name = "2 YEAR"
                },
                new Bond
                {
                    Id = 6,
                    Name = "5 YEAR"
                },
                new Bond
                {
                    Id = 7,
                    Name = "10 YEAR"
                },
                new Bond
                {
                    Id = 8,
                    Name = "30 YEAR"
                }
                );
        }
    }
}
