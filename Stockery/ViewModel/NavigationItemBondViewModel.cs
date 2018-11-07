using Prism.Commands;
using Prism.Events;
using Stockery.Event;
using System.Windows.Input;

namespace Stockery.ViewModel
{
    public class NavigationItemBondViewModel : ViewModelBase
    {
        private string _displayMember;
        private IEventAggregator _eventAggregator;

        public NavigationItemBondViewModel(int id, string displayMember, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            Id = id;
            DisplayMember = displayMember;
            OpenBondDetailViewCommand = new DelegateCommand(OnOpenBondDetailView);
        }

        public int Id { get; }

        public string DisplayMember
        {
            get { return _displayMember; }
            set
            {
                _displayMember = value;
                OnPropertyChanged();
            }
        }
        public ICommand OpenBondDetailViewCommand { get; }


        private void OnOpenBondDetailView()
        {
            _eventAggregator.GetEvent<OpenBondDetailViewEvent>().Publish(Id);
        }
    }
}
