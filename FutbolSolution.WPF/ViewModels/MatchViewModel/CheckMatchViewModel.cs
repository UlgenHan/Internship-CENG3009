using FutbolSolution.Core.DTOs.MatchDTOs;
using FutbolSolution.Core.Services;
using FutbolSolution.WPF.Services.Navigation;
using FutbolSolution.WPF.Utils;
using FutbolSolution.WPF.Views.MatchView;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace FutbolSolution.WPF.ViewModels.MatchView
{
    public class CheckMatchViewModel : INotifyPropertyChanged, INavigable
    {
        private MatchHolder _match;
        private MatchStatsDTO _matchStats;
        private IMatchStatisticsService _matchStatisticsService;
        private IMatchService _matchService;
        private INavigationService _navigationService;

        public MatchHolder Match
        {
            get => _match;
            set
            {
                _match = value;
                OnPropertyChanged();
            }
        }

        public MatchStatsDTO MatchStats
        {
            get => _matchStats;
            set
            {
                _matchStats = value;
                OnPropertyChanged();
            }
        }

        public ICommand UpdateMatchStatsCommand { get; }

        public CheckMatchViewModel(IMatchService matchService, IMatchStatisticsService matchStatisticsService, INavigationService navigationService)
        {
            _matchStatisticsService = matchStatisticsService;
            _matchService = matchService;
            // Initialize Match and MatchStats with sample data or fetch from a service
            //Match = new MatchDTO
            //{
            //    HomeTeamId = 1,
            //    AwayTeamId = 2,
            //    MatchDate = DateTime.Now,
            //    Stadium = "Sample Stadium",
            //    RefereeId = 101,
            //    WeatherConditions = "Clear",
            //    Importance = "High"
            //};

            //MatchStats = new MatchStatsDTO
            //{
            //    HomeGoals = 0,
            //    AwayGoals = 0,
            //    HomePossession = 50,
            //    AwayPossession = 50,
            //    HomeShots = 5,
            //    AwayShots = 5,
            //    HomeYellowCards = 0,
            //    AwayYellowCards = 0,
            //    HomeRedCards = 0,
            //    AwayRedCards = 0
            //};

            UpdateMatchStatsCommand = new RelayCommand<object>(UpdateMatchStats);
            _navigationService = navigationService;
        }

        private void UpdateMatchStats(object _)
        {
            _navigationService.NavigateTo<UpdateMatchStatsView>(MatchStats);
            OnPropertyChanged(nameof(MatchStats));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void OnNavigatedTo(object parameter)
        {
            var match = parameter as MatchHolder;
            Match = match;
            //var matchResponse = await _matchService.GetById(match.MatchID);
            var matchStatsResponse = await _matchStatisticsService.GetMatchStatsByMatchID(match.MatchID);

            if (matchStatsResponse.IsSuccessful && matchStatsResponse.StatusCode)
            {
                MatchStats = (MatchStatsDTO)matchStatsResponse.Data;
            }
            //if(matchResponse.IsSuccessful && matchResponse.StatusCode)
            //{
            //    Match = (MatchDTO)matchResponse.Data;
            //}
        }

        public void OnNavigatedFrom()
        {
            throw new NotImplementedException();
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }
    }
}
