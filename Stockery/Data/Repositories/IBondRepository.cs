using System.Collections.Generic;
using System.Threading.Tasks;
using Stockery.Model;

namespace Stockery.Data.Repositories
{
    public interface IBondRepository
    {
        Task<Bond> GetByIdAsync(int? bondId);
        Task SaveAsync();
        bool HasChanges();
        void Add(Bond bond);
        void Remove(Bond bond);
    }
}