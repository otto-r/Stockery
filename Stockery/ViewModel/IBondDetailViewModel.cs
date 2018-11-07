using System.Threading.Tasks;

namespace Stockery.ViewModel
{
    public interface IBondDetailViewModel
    {
        Task LoadAsync(int? bondId);
        bool HasChanges { get;  }
    }
}