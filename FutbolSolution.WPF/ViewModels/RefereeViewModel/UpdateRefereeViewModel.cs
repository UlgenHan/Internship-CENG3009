using FutbolSolution.Core.DTOs.RefereeDTOs;
using FutbolSolution.Core.Models;
using FutbolSolution.Core.Services;
using FutbolSolution.WPF.Services.Navigation;
using FutbolSolution.WPF.Utils;
using FutbolSolution.WPF.Validations;
using FutbolSolution.WPF.Windows;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace FutbolSolution.WPF.ViewModels.RefereeViewModel
{
    public class UpdateRefereeViewModel : INotifyPropertyChanged, INavigable
    {
        private readonly IRefereeService _refereeService;
        private readonly RefereeValidator _refereeValidator; // Add the validator
        private readonly INavigationService _navigationService;

        private string _name;
        private string _surname;
        private string _nationality;
        private decimal? _experienceYears;
        private decimal? _bias;
        private int _refereeId; // Changed to private field

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                OnPropertyChanged();
            }
        }

        public string Nationality
        {
            get => _nationality;
            set
            {
                _nationality = value;
                OnPropertyChanged();
            }
        }

        public decimal? ExperienceYears
        {
            get => _experienceYears;
            set
            {
                _experienceYears = value;
                OnPropertyChanged();
            }
        }

        public decimal? Bias
        {
            get => _bias;
            set
            {
                _bias = value;
                OnPropertyChanged();
            }
        }

        public ICommand UpdateCommand { get; }

        public UpdateRefereeViewModel(IRefereeService refereeService, INavigationService navigationService)
        {
            _refereeService = refereeService;
            _refereeValidator = new RefereeValidator(); // Initialize the validator
            UpdateCommand = new RelayCommand<object>(Update);
            _navigationService = navigationService;
        }

        private async void Update(object parameter)
        {
            var refereeDto = new RefereeDTO
            {
                RefereeId = _refereeId,
                Name = _name,
                Surname = _surname,
                Nationality = _nationality,
                ExperienceYears = _experienceYears,
                Bias = _bias
            };

            // Validate the refereeDto
            var (isValid, validationMessages) = _refereeValidator.Validate(new Referee
            {
                Name = refereeDto.Name,
                Surname = refereeDto.Surname,
                Nationality = refereeDto.Nationality,
                ExperienceYears = refereeDto.ExperienceYears,
                Bias = refereeDto.Bias
            });

            if (!isValid)
            {
                // Collect all validation messages into a single string
                var errorMessages = string.Join(Environment.NewLine,
                    validationMessages.Select(m => $"{m.Key}: {m.Value}"));

                // Show the error messages in the custom message box
                var errorMessageBox = new DarkThemeMessageBox(errorMessages,_navigationService);
                errorMessageBox.ShowDialog();
                return; // Stop further processing if validation fails
            }

            try
            {
                await _refereeService.Update(refereeDto);

                // Show success message
                var successMessage = "Data updated successfully!";
                var successMessageBox = new DarkThemeMessageBox(successMessage, _navigationService);
                successMessageBox.ShowDialog();

                // Clear fields after successful update (if desired)
                Name = string.Empty;
                Surname = string.Empty;
                Nationality = string.Empty;
                ExperienceYears = null;
                Bias = null;
            }
            catch (Exception ex)
            {
                var errorMessageBox = new DarkThemeMessageBox("An error occurred while updating the referee: " + ex.Message,_navigationService);
                errorMessageBox.ShowDialog();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void OnNavigatedTo(object parameter)
        {
            var referee = (RefereeDTO)parameter;
            _name = referee.Name;
            _surname = referee.Surname;
            _nationality = referee.Nationality;
            _bias = referee.Bias;
            _experienceYears = referee.ExperienceYears;
            _refereeId = referee.RefereeId; // Set the RefereeId
        }

        public void OnNavigatedFrom()
        {
            // Implement any logic needed when navigating away from this view
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }
    }
}
