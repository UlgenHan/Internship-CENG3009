using FutbolSolution.Core.DTOs.PlayerDTOs;
using FutbolSolution.WPF.Utils;
using FutbolSolution.Core.Services;
using FutbolSolution.WPF.Services.Navigation;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using FutbolSolution.WPF.Windows;
using FutbolSolution.Core.Validations;
using FutbolSolution.Core.Models;

namespace FutbolSolution.WPF.ViewModels.PlayerViewModel
{
    public class UpdatePlayerViewModel : INotifyPropertyChanged, INavigable
    {
        private IPlayerService _playerService;
        private PlayerValidator _playerValidator; // Assuming you have a validator interface
        private PlayerDTO _playerDTOUp;
        private INavigationService _navigationService;

        public PlayerDTO PlayerDTOUp
        {
            get => _playerDTOUp;
            set
            {
                _playerDTOUp = value;
                OnPropertyChanged();
            }
        }

        private int playerStatsId;
        private byte[] playerImage;
        private bool isFileUploaded;

        public int PlayerStatsId
        {
            get => playerStatsId;
            set { playerStatsId = value; OnPropertyChanged(nameof(PlayerStatsId)); _playerDTOUp.PlayerStatsId = value; }
        }

        public byte[] PlayerImage
        {
            get => playerImage;
            set { playerImage = value; OnPropertyChanged(nameof(PlayerImage)); }
        }

        public bool IsFileUploaded
        {
            get => isFileUploaded;
            set { isFileUploaded = value; OnPropertyChanged(nameof(IsFileUploaded)); }
        }

        public ICommand UpdateCommand { get; }

        public UpdatePlayerViewModel(IPlayerService playerService, INavigationService navigationService)
        {
            _playerService = playerService;
            _playerValidator = new PlayerValidator();
            UpdateCommand = new RelayCommand<object>(UpdatePlayer);
            _navigationService = navigationService;
        }

        private async void UpdatePlayer(object parameter)
        {
           

            // Validate the playerDto
            var (isValid, validationMessages) = _playerValidator.Validate(new Player
            {
                Name = PlayerDTOUp.Name,
                Surname = PlayerDTOUp.Surname,
                Age = PlayerDTOUp.Age,
                DateOfBirth = PlayerDTOUp.DateOfBirth,
                Nationality = PlayerDTOUp.Nationality,
                Position = PlayerDTOUp.Position,
                CurrentClub = PlayerDTOUp.CurrentClub,
                Height = PlayerDTOUp.Height,
                Weight = PlayerDTOUp.Weight,
                PreferredFoot = PlayerDTOUp.PreferredFoot
            });
          
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
                if (isFileUploaded)
                {
                    PlayerDTOUp.PlayerImage.ImageData = playerImage;
                }
                await _playerService.Update(PlayerDTOUp);

                // Show success message
                var successMessage = "Player updated successfully!";
                var successMessageBox = new DarkThemeMessageBox(successMessage, _navigationService);
                successMessageBox.ShowDialog();
            }
            catch (Exception ex)
            {
                var errorMessageBox = new DarkThemeMessageBox("An error occurred while updating the player: " + ex.Message, _navigationService);
                errorMessageBox.ShowDialog();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void OnNavigatedTo(object parameter)
        {
            var player = parameter as PlayerDTO;
            if (player != null)
            {
                PlayerDTOUp = player;
            }
        }

        public void OnNavigatedFrom()
        {
            
        }

        public void Refresh()
        {
            
        }
    }
}
