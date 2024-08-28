using FutbolSolution.Core.DTOs.InjuresSuspensionDTOs;
using FutbolSolution.Core.DTOs.PlayerDTOs;
using FutbolSolution.Core.Models;
using FutbolSolution.Core.Services;
using FutbolSolution.Core.Validations;
using FutbolSolution.WPF.Services.Navigation;
using FutbolSolution.WPF.Utils;
using FutbolSolution.WPF.Windows;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace FutbolSolution.WPF.ViewModels.InjuresSuspensionViewModel
{
    public class CreateInjuresSuspensionViewModel : INotifyPropertyChanged, INavigable
    {
        private string _type;
        private string _description;
        private DateTime _startDate;
        private DateTime _endDate;
        private PlayerDTO _player { get; set; }
        private INavigationService _navigationService { get; set; }
        private IInjuresSuspensionService _injuresSuspensionService { get; set; }
        private InjuriesSuspensionsValidator _injuriesSuspensionsValidator;
        private int _selectedSeverityIndex;
        public int SelectedSeverityIndex
        {
            get => _selectedSeverityIndex;
            set
            {
                _selectedSeverityIndex = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _severityItems;

        public ObservableCollection<string> SeverityItems
        {
            get => _severityItems;
            set
            {
                _severityItems = value;
                OnPropertyChanged();
            }
        }

        public string Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged();
            }
        }

        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged();
            }
        }

        public ICommand SubmitCommand { get; }

        public CreateInjuresSuspensionViewModel(INavigationService navigationService, IInjuresSuspensionService injuresSuspensionService)
        {
            _navigationService = navigationService;
            _injuresSuspensionService = injuresSuspensionService;
            _injuriesSuspensionsValidator = new InjuriesSuspensionsValidator(); // Initialize the validator
            SubmitCommand = new RelayCommand<object>(OnSubmit);

            SeverityItems = new ObservableCollection<string> { "Low", "Mid", "High" };
            SelectedSeverityIndex = -1;
        }

        private async void OnSubmit(object obj)
        {

            Console.WriteLine(SelectedSeverityIndex.ToString());

            var injuresCreateDTO = new InjuriesSuspensionsDTO
            {
                Description = Description,
                EndDate = EndDate,
                StartDate = StartDate,
                Type = Type,
                PlayerId = _player.Id
            };

            

            // Validate the injuresCreateDTO
            var (isValid, validationMessages) = _injuriesSuspensionsValidator.Validate(new InjuriesSuspensions
            {
                Description = injuresCreateDTO.Description,
                StartDate = injuresCreateDTO.StartDate,
                EndDate = injuresCreateDTO.EndDate,
                Type = injuresCreateDTO.Type,
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
                injuresCreateDTO.InjuriesLink = new InjuriesLink { Severity = SelectedSeverityIndex };
                await _injuresSuspensionService.Create(injuresCreateDTO);
                

                // Show success message
                var successMessage = "Injury/Suspension created successfully!";
                var successMessageBox = new DarkThemeMessageBox(successMessage, _navigationService);
                successMessageBox.ShowDialog();

                // Clear fields after successful creation
                Type = string.Empty;
                Description = string.Empty;
                StartDate = DateTime.Now;
                EndDate = DateTime.Now;
            }
            catch (Exception ex)
            {
                var errorMessageBox = new DarkThemeMessageBox("An error occurred while creating the injury/suspension: " + ex.Message, _navigationService);
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
            var playerDTO = parameter as PlayerDTO;
            if (playerDTO != null)
            {
                _player = playerDTO;
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
