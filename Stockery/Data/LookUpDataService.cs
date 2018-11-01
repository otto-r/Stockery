using Stockery.DataAccess;
using Stockery.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stockery.Data
{
    public class LookUpDataService : IStockLookUpDataService
    {
        private Func<StockDbContext> _contextCreator;

        public LookUpDataService(Func<StockDbContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }

        public async Task<IEnumerable<LookUpItem>> GetStockLookUpAsync()
        {
            using(var context = _contextCreator())
            {
                return await context.Stocks.AsNoTracking()
                    .Select(s => new LookUpItem
                    {
                        Id = s.Id,
                        DisplayMember = s.Name + " - " + s.Ticker
                    }).ToListAsync();
            }
        }
    }
}
