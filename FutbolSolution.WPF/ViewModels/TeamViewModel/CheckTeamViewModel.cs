using FutbolSolution.Core.DTOs.TeamDTOs;
using FutbolSolution.WPF.Services.Navigation;
using FutbolSolution.WPF.Utils;
using FutbolSolution.WPF.Views.TeamView;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace FutbolSolution.WPF.ViewModels.TeamViewModel
{
    public class CheckTeamViewModel : INotifyPropertyChanged, INavigable
    {
        private TeamDTO _team;
        private TeamWithStatisticsDTO _teamStats;
        private ObservableCollection<Item> _items;
        private INavigationService _navigationService;

        public TeamDTO Team
        {
            get { return _team; }
            set
            {
                _team = value;
                OnPropertyChanged();
            }
        }

        public TeamWithStatisticsDTO TeamStats
        {
            get { return _teamStats; }
            set
            {
                _teamStats = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Item> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        public ICommand UpdateStatsCommand { get; set; }
        public ICommand AddPlayerCommand { get; set; }

        public CheckTeamViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Items = new ObservableCollection<Item>
            {
                new Item { Id = 1, Name = "Player 1" },
                new Item { Id = 2, Name = "Player 2" },
                new Item { Id = 3, Name = "Player 3" }
            };

            UpdateStatsCommand = new RelayCommand<object>(UpdateStatsNavigation);
        }

   

        private void UpdateStatsNavigation(object obj)
        {
            _navigationService.NavigateTo<UpdateTeamStatsView>(_teamStats);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void OnNavigatedTo(object parameter)
        {
            if (parameter == null)
            {
                return;
            }

            var teamProperty = parameter.GetType().GetProperty("teamDTO");
            var teamStatsProperty = parameter.GetType().GetProperty("teamStats");

            if (teamProperty != null || teamStatsProperty != null)
            {
                var team = teamProperty.GetValue(parameter);
                var teamStats = teamStatsProperty.GetValue(parameter);

                _team = (TeamDTO)team;
                _teamStats = (TeamWithStatisticsDTO)teamStats;
            }
        }

        public void OnNavigatedFrom()
        {
            //
        }

        public void Refresh()
        {
            // Implement any refresh logic if needed
        }

        public class Item
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

    }
}
