
using FutbolSolution.Core.DTOs.MatchDTOs;
using FutbolSolution.Core.Services;
using FutbolSolution.Core.Validations;
using FutbolSolution.WPF.Services.Navigation;
using FutbolSolution.WPF.Utils;
using FutbolSolution.WPF.Validations;
using FutbolSolution.WPF.Windows;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace FutbolSolution.WPF.ViewModels.MatchViewModel
{
    public class UpdateMatchStatsViewModel : INotifyPropertyChanged, INavigable
    {
        private IMatchStatisticsService _matchStatisticsService;
        private readonly INavigationService _navigationService;
        private MatchStatsValidator _entityValidator;

        private MatchStatsDTO _matchStats;

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

        public UpdateMatchStatsViewModel(IMatchStatisticsService matchStatisticsService,
            MatchStatsValidator entityValidator,INavigationService navigationService)
        {
            _matchStatisticsService = matchStatisticsService;
            UpdateMatchStatsCommand = new RelayCommand<object>(UpdateMatchStats);
            _navigationService = navigationService;
            _entityValidator = entityValidator;
        }

        //private async void UpdateMatchStats(object _)
        //{
        //    // Logic to update match stats (e.g., saving to a database)
        //    // Implement your data-saving logic here
        //    await _matchStatisticsService.Update(MatchStats);
        //    // Notify that the stats have been updated (for example, show a message)
        //    OnPropertyChanged(nameof(MatchStats));
        //}

        private async void UpdateMatchStats(object _)
        {
            try
            {
                // Validate MatchStats
                var (isValid, validationMessages) = _entityValidator.Validate(MatchStats);

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
                var succesMessageBox = new DarkThemeMessageBox("Data updated!",_navigationService);
                succesMessageBox.ShowDialog();
                // If valid, proceed to update match stats
                await _matchStatisticsService.Update(MatchStats);

                // Notify that the stats have been updated (for example, show a message)
                OnPropertyChanged(nameof(MatchStats));
            }
            catch (Exception ex)
            {
                var errorMessageBox = new DarkThemeMessageBox("You should enter number, not a string!!", _navigationService);
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
            var matchStatsDto = parameter as MatchStatsDTO;
            MatchStats = matchStatsDto;
        }

        public void OnNavigatedFrom()
        {
            // ??
        }

        public void Refresh()
        {
            // ??
        }
    }
}
