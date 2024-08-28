using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using FutbolSolution.Core.DTOs.TeamDTOs;
using FutbolSolution.Core.Models;
using FutbolSolution.Core.Services;
using FutbolSolution.Core.Validations;
using FutbolSolution.Repository;
using FutbolSolution.WPF.Services.Navigation;
using FutbolSolution.WPF.Utils;
using FutbolSolution.WPF.Windows;
namespace FutbolSolution.WPF.ViewModels.TeamViewModel
{
    public class UpdateTeamStatsViewModel : INotifyPropertyChanged, INavigable
    {
        private INavigationService _navigationService;
        private ITeamStatisticsService _teamStatisticsService;
        private TeamStatisticsValidator _teamStatisticsValidator;
        private TeamWithStatisticsDTO _teamStats;

        public TeamWithStatisticsDTO TeamStats
        {
            get => _teamStats;
            set
            {
                _teamStats = value;
                OnPropertyChanged();
            }
        }

        public ICommand UpdateCommand { get; set; }

        public UpdateTeamStatsViewModel(ITeamStatisticsService teamStatisticsService,
            INavigationService navigationService,
            TeamStatisticsValidator teamStatisticsValidator)
        {
            _navigationService = navigationService;
            _teamStatisticsService = teamStatisticsService;
            TeamStats = new TeamWithStatisticsDTO(); // Initialize the TeamStatsDTO
            UpdateCommand = new RelayCommand<object>(UpdateStats);
            _teamStatisticsValidator = teamStatisticsValidator;
        }


        private async void UpdateStats(object parameter = null)
        {
            // Convert TeamWithStatisticsDTO to TeamStatistics for validation
            var teamStatistics = new TeamStatistics
            {
                GoalsScored = TeamStats.GoalsScored,
                GoalsConceded = TeamStats.GoalsConceded,
                Wins = TeamStats.Wins,
                Draws = TeamStats.Draws,
                Losses = TeamStats.Losses,
                HomeWins = TeamStats.HomeWins,
                AwayWins = TeamStats.AwayWins,
                RecentForm = TeamStats.RecentForm,
                SeasonYear = TeamStats.SeasonYear,
                TeamId = TeamStats.TeamId,
                TeamStatsId = TeamStats.TeamStatsId,
            };

            // Validate the team statistics
            var (isValid, validationMessages) = _teamStatisticsValidator.Validate(teamStatistics);

            if (!isValid)
            {
                var errorMessages = string.Join(Environment.NewLine,
                    validationMessages.Select(m => $"{m.Key}: {m.Value}"));
                
                var errorMessageBox = new DarkThemeMessageBox(errorMessages, _navigationService);
                errorMessageBox.ShowDialog();
                return;
            }

            try
            {
                await _teamStatisticsService.Update(TeamStats);

                var successMessage = "Team statistics updated successfully!";
                var successMessageBox = new DarkThemeMessageBox(successMessage, _navigationService);
                successMessageBox.ShowDialog();
            }
            catch (Exception ex)
            {
                var errorMessageBox = new DarkThemeMessageBox("An error occurred while updating the team statistics: " + ex.Message, _navigationService);
                errorMessageBox.ShowDialog();
            }
        }

        private string _message;
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void OnNavigatedTo(object parameter)
        {
            var teamStat = (TeamWithStatisticsDTO)parameter;
            _teamStats = teamStat;
        }

        public void OnNavigatedFrom()
        {
            //
        }

        public void Refresh()
        {
           //
        }
    }
}
