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
using System.Windows;
using System.Windows.Input;

namespace FutbolSolution.WPF.ViewModels.RefereeViewModel
{
    public class CreateRefereeViewModel : INotifyPropertyChanged
    {
        private readonly IRefereeService _refereeService;
        private readonly RefereeValidator _refereeValidator;
        private INavigationService _navigationService;

        private string _name;
        private string _surname;
        private string _nationality;
        private decimal? _experienceYears;
        private decimal? _bias;

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

        public ICommand SaveCommand { get; }

        public CreateRefereeViewModel(IRefereeService refereeService,INavigationService navigationService)
        {
            _navigationService = navigationService;
            _refereeService = refereeService;

            //RefereeValidator used for data validation 
            //Not Factored
            _refereeValidator = new RefereeValidator();
            SaveCommand = new RelayCommand<RefereeDTO>(Save);
        }

        private async void Save(object parameter)
        {
            var refereeDto = new RefereeDTO
            {
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
                //Do not use default messageBox,we have custom design for it.
                var errorMessageBox = new DarkThemeMessageBox(errorMessages,_navigationService);
                errorMessageBox.ShowDialog();
                return;
            }

            try
            {
                await _refereeService.Create(refereeDto);

                // Show success message
                var successMessage = "Data added successfully!";
                var successMessageBox = new DarkThemeMessageBox(successMessage,_navigationService);
                successMessageBox.ShowDialog();

                // Clear fields after successful save
                Name = string.Empty;
                Surname = string.Empty;
                Nationality = string.Empty;
                ExperienceYears = null;
                Bias = null;
            }
            catch (Exception ex)
            {
                //Exception can be thrown by the oracle db connection issue
                //Temporary Solution: Create Singleton AppDbContext
                var errorMessageBox = new DarkThemeMessageBox("An error occurred while saving the referee: " + ex.Message,_navigationService);
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
