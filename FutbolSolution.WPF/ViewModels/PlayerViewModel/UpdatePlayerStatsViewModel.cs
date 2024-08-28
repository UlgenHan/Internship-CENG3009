using FutbolSolution.Core.DTOs.PlayerDTOs;
using FutbolSolution.Core.Models;
using FutbolSolution.Core.Services;
using FutbolSolution.Core.Validations;
using FutbolSolution.WPF.Services.Navigation;
using FutbolSolution.WPF.Utils;
using FutbolSolution.WPF.Windows;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace FutbolSolution.WPF.ViewModels.PlayerViewModel
{
    public class UpdatePlayerStatsViewModel : INotifyPropertyChanged , INavigable
    {
        private readonly IPlayerStatsService _playerStatsService;
        private readonly PlayerStatsValidator _playerStatsValidator;
        private INavigationService _navigationService;

        private PlayerStatsDTO _playerStats;

        public PlayerStatsDTO PlayerStats
        {
            get => _playerStats;
            set
            {
                _playerStats = value;
                OnPropertyChanged();
            }
        }

        public ICommand UpdateCommand { get; }

        public UpdatePlayerStatsViewModel(IPlayerStatsService playerStatsService, INavigationService navigationService)
        {
            _playerStatsService = playerStatsService;
            _navigationService = navigationService;
            _playerStatsValidator = new PlayerStatsValidator(); // Initialize the validator
            UpdateCommand = new RelayCommand<object>(UpdateStats);
        }

        private async void UpdateStats(object parameter)
        {
            // Validate player statistics before updating
            var (isValid, validationMessages) = _playerStatsValidator.Validate(MapToPlayerStats());

            if (!isValid)
            {
                // Collect all validation messages into a single string
                var errorMessages = string.Join(Environment.NewLine,
                    validationMessages.Select(m => $"{m.Key}: {m.Value}"));

                // Show the error messages in the custom message box
                var errorMessageBox = new DarkThemeMessageBox(errorMessages, _navigationService);
                errorMessageBox.ShowDialog();
                return; // Stop further processing if validation fails
            }

            try
            {
                await _playerStatsService.Update(PlayerStats);

                // Show success message
                var successMessage = "Player statistics updated successfully!";
                var successMessageBox = new DarkThemeMessageBox(successMessage, _navigationService);
                successMessageBox.ShowDialog();
            }
            catch (Exception ex)
            {
                var errorMessageBox = new DarkThemeMessageBox("An error occurred while updating player statistics: " + ex.Message, _navigationService);
                errorMessageBox.ShowDialog();
            }
        }

        private PlayerStats MapToPlayerStats()
        {
            // Map PlayerStatsDTO to PlayerStats model
            return new PlayerStats
            {
                PlayerStatsId = PlayerStats.Id,
                Goals = PlayerStats.Goals,
                Assists = PlayerStats.Assists,
                TotalMinutesIn = PlayerStats.TotalMinutesIn,
                PassAccuracy = PlayerStats.PassAccuracy,
                Tackles = PlayerStats.Tackles,
                Interceptions = PlayerStats.Interceptions,
                Clearances = PlayerStats.Clearances,
                Shots = PlayerStats.Shots,
                ShotsOnTarget = PlayerStats.ShotsOnTarget,
                DribblesCompleted = PlayerStats.DribblesCompleted,
                AerialDuelsWon = PlayerStats.AerialDuelsWon,
                YellowCards = PlayerStats.YellowCards,
                RedCards = PlayerStats.RedCards,
                FoulsCommitted = PlayerStats.FoulsCommitted,
                FoulsSuffered = PlayerStats.FoulsSuffered,
                Offsides = PlayerStats.Offsides,
                Saves = PlayerStats.Saves,
                CleanSheets = PlayerStats.CleanSheets
            };
        }

        public void OnNavigatedTo(object parameter)
        {
            var playerStatDTO = parameter as PlayerStatsDTO;
            if (playerStatDTO != null)
            {
                _playerStats = playerStatDTO;
            }
        }

        public void OnNavigatedFrom()
        {
            throw new NotImplementedException();
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
