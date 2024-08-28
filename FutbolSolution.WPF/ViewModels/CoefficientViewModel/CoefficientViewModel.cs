using FutbolSolution.WPF.Services.Navigation;
using FutbolSolution.WPF.Utils;
using FutbolSolution.WPF.Windows;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace FutbolSolution.WPF.ViewModels.CoefficientViewModel
{
    public class CoefficientViewModel : INotifyPropertyChanged
    {
        private readonly INavigationService _navigationService;
        private decimal _intercept;
        private decimal _betaTeamStrength;
        private decimal _betaTeamStats;
        private decimal _betaRefBias;
        private decimal _betaMatchHistory;

        public decimal Intercept
        {
            get => _intercept;
            set { _intercept = value; OnPropertyChanged(); }
        }

        public decimal BetaTeamStrength
        {
            get => _betaTeamStrength;
            set { _betaTeamStrength = value; OnPropertyChanged(); }
        }

        public decimal BetaTeamStats
        {
            get => _betaTeamStats;
            set { _betaTeamStats = value; OnPropertyChanged(); }
        }

        public decimal BetaRefBias
        {
            get => _betaRefBias;
            set { _betaRefBias = value; OnPropertyChanged(); }
        }

        public decimal BetaMatchHistory
        {
            get => _betaMatchHistory;
            set { _betaMatchHistory = value; OnPropertyChanged(); }
        }

        public ICommand SaveCommand { get; }


        public CoefficientViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            SaveCommand = new RelayCommand<object>(SaveConfiguration);

        }

        private void SaveConfiguration(object _)
        {
            try
            {
                CoefficientHolder.Intercept = Intercept;
                CoefficientHolder.BetaMatchHistory = BetaMatchHistory;
                CoefficientHolder.BetaTeamStats = BetaTeamStats;
                CoefficientHolder.BetaRefBias = BetaRefBias;
                CoefficientHolder.BetaTeamStrength = BetaTeamStrength;

                var messageDialog = new DarkThemeMessageBox("Coefficients Updated !!", _navigationService);
                messageDialog.ShowDialog();
            }catch (Exception ex)
            {
                var messageDialog = new DarkThemeMessageBox("Coefficients should be decimal!!", _navigationService);
                messageDialog.ShowDialog();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
