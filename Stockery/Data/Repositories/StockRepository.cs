using Stockery.DataAccess;
using Stockery.Model;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Stockery.Data.Repositories
{
    public class StockRepository : IStockRepository
    {
        private StockDbContext _context;

        public StockRepository(StockDbContext context)
        {
            _context = context;
        }

        public void Add(Stock stock)
        {
            _context.Stocks.Add(stock);
        }

        public async Task<Stock> GetByIdAsync(int? stockId)
        {

            return await _context.Stocks.SingleAsync(s => s.Id == stockId);
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public void Remove(Stock model)
        {
            _context.Stocks.Remove(model);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
