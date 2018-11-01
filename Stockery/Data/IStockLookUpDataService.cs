using System.Collections.Generic;
using System.Threading.Tasks;
using Stockery.Model;

namespace Stockery.Data
{
    public interface IStockLookUpDataService
    {
        Task<IEnumerable<LookUpItem>> GetStockLookUpAsync();
    }
}