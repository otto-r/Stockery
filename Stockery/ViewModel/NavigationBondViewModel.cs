using Prism.Events;
using Stockery.Data.LookUps;
using Stockery.Event;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Stockery.ViewModel
{
    public class NavigationBondViewModel : ViewModelBase, INavigationBondViewModel
    {
        private IBondLookUpDataService _bondLookUpDataService;
        private IEventAggregator _eventAggregator;

        public NavigationBondViewModel(IBondLookUpDataService bondLookUpDataService, IEventAggregator eventAggregator)
        {
            _bondLookUpDataService = bondLookUpDataService;
            _eventAggregator = eventAggregator;
            Bonds = new ObservableCollection<NavigationItemBondViewModel>();
            _eventAggregator.GetEvent<AfterBondSavedEvent>().Subscribe(AfterBondSaved);
            _eventAggregator.GetEvent<AfterBondDeletedEvent>().Subscribe(AfterBondDeleted);
        }

        public async Task LoadAsync()
        {
            var lookup = await _bondLookUpDataService.GetBondLookUpAsync();
            Bonds.Clear();
            foreach (var item in lookup)
            {
                Bonds.Add(new NavigationItemBondViewModel(item.Id, item.DisplayMember, _eventAggregator));
            }
        }
        public ObservableCollection<NavigationItemBondViewModel> Bonds { get; }

        private NavigationItemBondViewModel _selectedBond;

        public NavigationItemBondViewModel SelectedBond
        {
            get { return _selectedBond; }
            set
            {
                _selectedBond = value;
                OnPropertyChanged();
                if (_selectedBond != null)
                {
                    _eventAggregator.GetEvent<OpenBondDetailViewEvent>().Publish(_selectedBond.Id);
                }
            }
        }

        private void AfterBondDeleted(int bondId)
        {
            var bond = Bonds.SingleOrDefault(s => s.Id == bondId);
            if (bond!=null)
            {
                Bonds.Remove(bond);
            }
        }

        private void AfterBondSaved(AfterBondSavedEventArgs obj)
        {
            var lookUpItem = Bonds.SingleOrDefault(s => s.Id == obj.Id);
            if (lookUpItem == null)
            {
                Bonds.Add(new NavigationItemBondViewModel(obj.Id, obj.DisplayMember, _eventAggregator));
            }
            else
            {
                lookUpItem.DisplayMember = obj.DisplayMember;
            }
        }
    }
}
