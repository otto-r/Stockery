using Stockery.DataAccess;
using Stockery.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stockery.Data.LookUps
{
    public class LookUpBondDataService : IBondLookUpDataService
    {
        private Func<StockDbContext> _contextCreator;

        public LookUpBondDataService(Func<StockDbContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }

        public async Task<IEnumerable<LookUpItem>> GetBondLookUpAsync()
        {
            using(var context = _contextCreator())
            {
                return await context.Bonds.AsNoTracking()
                    .Select(b => new LookUpItem
                    {
                        Id = b.Id,
                        DisplayMember = b.Name
                    }).ToListAsync();
            }
        }
    }
}
