using FutbolSolution.WPF.Services.Navigation;
using FutbolSolution.WPF.ViewModels.MainViewModel;
using FutbolSolution.WPF.Views.CoefficientView;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace FutbolSolution.WPF.Views.MainView
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl, INavigable
    {
        private readonly INavigationService _navigationService;
        private readonly MainViewModel _mainViewModel;
        public MainView(INavigationService navigationService,MainViewModel mainViewModel)
        {
            InitializeComponent();
            _navigationService = navigationService;
            _mainViewModel = mainViewModel;

            DataContext = _mainViewModel;
        }

        public void OnNavigatedFrom()
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(object parameter)
        {
            // Handle any parameters passed during navigation if needed
        }

        public void Refresh()
        {
            //
        }

        private void NavigateToSecondView_Click(object sender, RoutedEventArgs e)
        {
            //// Pass a message parameter to the second view
            //_navigationService.NavigateTo<SecondView>("Hello from MainView");
        }

        private void TrendingCards_MouseDoubleClick_Sim(object sender, MouseButtonEventArgs e)
        {
            _navigationService.NavigateTo<AnalysisView.AnalysisView>(null);
        }

        private void TrendingCards_MouseDoubleClick_Coefficient(object sender, MouseButtonEventArgs e)
        {
            _navigationService.NavigateTo<CoefficientView.CoefficientView>(null);
        }
    }
}
