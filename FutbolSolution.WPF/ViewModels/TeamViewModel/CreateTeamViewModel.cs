using FutbolSolution.Core.DTOs.TeamDTOs;
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

namespace FutbolSolution.WPF.ViewModels.TeamViewModel
{
    public class CreateTeamViewModel : INotifyPropertyChanged
    {
        private readonly ITeamService _teamService;
        private readonly TeamValidator _teamValidator;
        private INavigationService _navigationService;

        private string _name;
        private string _stadium;
        private string _coach;
        private decimal _foundedYear;
        private string _city;

        public bool IsFileUploaded {  get; set; }
        public TeamImage teamImage { get; set; } = new TeamImage();

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Stadium
        {
            get => _stadium;
            set
            {
                _stadium = value;
                OnPropertyChanged();
            }
        }

        public string Coach
        {
            get => _coach;
            set
            {
                _coach = value;
                OnPropertyChanged();
            }
        }

        public decimal FoundedYear
        {
            get => _foundedYear;
            set
            {
                _foundedYear = value;
                OnPropertyChanged();
            }
        }

        public string City
        {
            get => _city;
            set
            {
                _city = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }

        public CreateTeamViewModel(ITeamService teamService, INavigationService navigationService)
        {
            _teamService = teamService;
            _navigationService = navigationService;
            _teamValidator = new TeamValidator(); // Initialize the validator
            SaveCommand = new RelayCommand<object>(Save);
        }

        private async void Save(object parameter)
        {
            var teamDto = new TeamDTO
            {
                Name = _name,
                Stadium = _stadium,
                Coach = _coach,
                FoundedYear = _foundedYear,
                City = _city,
                TeamImage = teamImage
            };

            // Validate the teamDto
            var (isValid, validationMessages) = _teamValidator.Validate(new Team
            {
                Name = teamDto.Name,
                Stadium = teamDto.Stadium,
                Coach = teamDto.Coach,
                FoundedYear = teamDto.FoundedYear,
                City = teamDto.City
            });


            if (!isValid || !IsFileUploaded)
            {
                // Collect all validation messages into a single string
                var errorMessages = string.Join(Environment.NewLine,
                    validationMessages.Select(m => $"{m.Key}: {m.Value}"));

                if(!IsFileUploaded)
                {
                    errorMessages += Environment.NewLine;
                    errorMessages += "Team Image not loaded!";
                }
                // Show the error messages in the custom message box
                var errorMessageBox = new DarkThemeMessageBox(errorMessages, _navigationService);
                errorMessageBox.ShowDialog();
                return; // Stop further processing if validation fails
            }

            try
            {
                await _teamService.Create(teamDto);

                // Show success message
                var successMessage = "Team created successfully!";
                var successMessageBox = new DarkThemeMessageBox(successMessage, _navigationService);
                successMessageBox.ShowDialog();

                // Clear fields after successful save
                Name = string.Empty;
                Stadium = string.Empty;
                Coach = string.Empty;
                FoundedYear = 0;
                City = string.Empty;
            }
            catch (Exception ex)
            {
                var errorMessageBox = new DarkThemeMessageBox("An error occurred while saving the team: " + ex.Message, _navigationService);
                errorMessageBox.ShowDialog();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
