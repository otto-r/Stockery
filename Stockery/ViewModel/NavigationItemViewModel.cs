using Prism.Commands;
using Prism.Events;
using Stockery.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Stockery.ViewModel
{
    public class NavigationItemViewModel : ViewModelBase
    {
        private string _displayMember;
        private IEventAggregator _eventAggregator;

        public NavigationItemViewModel(int id, string displayMember, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            Id = id;
            DisplayMember = displayMember;
            OpenStockDetailViewCommand = new DelegateCommand(OnOpenStockDetailView);
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
        public ICommand OpenStockDetailViewCommand { get; }


        private void OnOpenStockDetailView()
        {
            _eventAggregator.GetEvent<OpenStockDetailViewEvent>().Publish(Id);
        }
    }
}
