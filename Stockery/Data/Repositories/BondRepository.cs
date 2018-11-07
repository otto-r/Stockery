using Stockery.DataAccess;
using Stockery.Model;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Stockery.Data.Repositories
{
    public class BondRepository : IBondRepository
    {
        private StockDbContext _context;

        public BondRepository(StockDbContext context)
        {
            _context = context;
        }

        public void Add(Bond bond)
        {
            _context.Bonds.Add(bond);
        }

        public async Task<Bond> GetByIdAsync(int? bondId)
        {

            return await _context.Bonds.SingleAsync(s => s.Id == bondId);
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public void Remove(Bond model)
        {
            _context.Bonds.Remove(model);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
