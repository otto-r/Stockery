using Prism.Commands;
using Prism.Events;
using Stockery.Data.Repositories;
using Stockery.Event;
using Stockery.Model;
using Stockery.Wrapper;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Stockery.ViewModel
{
    public class BondDetailViewModel : ViewModelBase, IBondDetailViewModel
    {
        private IBondRepository _bondRepository;
        private IEventAggregator _eventAggregator;
        private BondWrapper _bond;
        private bool _hasChanges;


        public BondDetailViewModel(IBondRepository bondRepository, IEventAggregator eventAggregator)
        {
            _bondRepository = bondRepository;
            _eventAggregator = eventAggregator;

            SaveBondCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            DeleteBondCommand = new DelegateCommand(OnDeleteExecute);
        }

        private async void OnDeleteExecute()
        {
            _bondRepository.Remove(Bond.Model);
            await _bondRepository.SaveAsync();
            _eventAggregator.GetEvent<AfterBondDeletedEvent>().Publish(Bond.Id);
        }

        public async Task LoadAsync(int? bondId)
        {
            var bond = bondId.HasValue
                ? await _bondRepository.GetByIdAsync(bondId.Value)
            : AddNewBond();
            Bond = new BondWrapper(bond);
            Bond.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _bondRepository.HasChanges();
                }
                if (e.PropertyName == nameof(Bond.HasErrors))
                {
                    ((DelegateCommand)SaveBondCommand).RaiseCanExecuteChanged();
                }
            };
            if (Bond.Id == 0)
            {
                Bond.Name = "";
            }
            ((DelegateCommand)SaveBondCommand).RaiseCanExecuteChanged();
        }


        public BondWrapper Bond
        {
            get { return _bond; }
            private set
            {
                _bond = value;
                OnPropertyChanged();
            }
        }

        public bool HasChanges
        {
            get { return _hasChanges; }
            set
            {
                if (_hasChanges != value)
                {
                    _hasChanges = value;
                    OnPropertyChanged();
                    ((DelegateCommand)SaveBondCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public ICommand SaveBondCommand { get; }
        public ICommand DeleteBondCommand { get; }

        private async void OnSaveExecute()
        {
            await _bondRepository.SaveAsync();
            HasChanges = _bondRepository.HasChanges();
            _eventAggregator.GetEvent<AfterBondSavedEvent>().Publish(new AfterBondSavedEventArgs
            {
                Id = Bond.Id,
                DisplayMember = $"{Bond.Name}"
            });
        }

        private bool OnSaveCanExecute()
        {
            return Bond != null && !Bond.HasErrors && HasChanges;
        }

        private Bond AddNewBond()
        {
            var bond = new Bond();
            _bondRepository.Add(bond);
            return bond;
        }
    }
}
