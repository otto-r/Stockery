using System.Collections.Generic;
using System.Threading.Tasks;
using Stockery.Model;

namespace Stockery
{
    public interface IStockDataService
    {
        Task<Stock> GetByIdAsync(int stockId);
    }
}