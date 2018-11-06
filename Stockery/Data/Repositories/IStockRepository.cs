using System.Collections.Generic;
using System.Threading.Tasks;
using Stockery.Model;

namespace Stockery.Data.Repositories
{
    public interface IStockRepository
    {
        Task<Stock> GetByIdAsync(int? stockId);
        Task SaveAsync();
        bool HasChanges();
        void Add(Stock stock);
        void Remove(Stock model);
    }
}