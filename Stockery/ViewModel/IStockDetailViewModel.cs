using System.Threading.Tasks;

namespace Stockery.ViewModel
{
    public interface IStockDetailViewModel
    {
        Task LoadAsync(int? stockId);
        bool HasChanges { get;  }
    }
}