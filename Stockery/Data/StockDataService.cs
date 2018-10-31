using Stockery.DataAccess;
using Stockery.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Stockery
{
    public class StockDataService : IStockDataService
    {
        private Func<StockDbContext> _contextCreator;

        public StockDataService(Func<StockDbContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }
        public async Task<List<Stock>> GetStocksAsync()
        {
            using (var context = new StockDbContext())
            {
                return await context.Stocks.AsNoTracking().ToListAsync();
            }
        }
    }
}
 