
using FutbolSolution.WPF.Services.Navigation;
using FutbolSolution.WPF.Services.Search;
using FutbolSolution.WPF.UserControls.SearchBox;
using FutbolSolution.WPF.ViewModels.AnalysisViewModel;
using FutbolSolution.WPF.ViewModels.SearchViewModel;
using FutbolSolution.WPF.Views.AnalysisView;
using FutbolSolution.WPF.Views.CoefficientView;
using FutbolSolution.WPF.Views.MainView;
using FutbolSolution.WPF.Views.MatchView;
using FutbolSolution.WPF.Views.PlayerView;
using FutbolSolution.WPF.Views.RefereeView;
using FutbolSolution.WPF.Views.TeamView;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using System.Windows.Input;

namespace FutbolSolution.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly INavigationService _navigationService;
        private readonly SearchViewModel _searchViewModel;
        private readonly ISearchService _searchService;
        public MainWindow()
        {
            InitializeComponent();
           
            // Retrieve the navigation service and search view model from the service provider
            _navigationService = Bootstrapper.ServiceProvider.GetRequiredService<INavigationService>();
            _searchViewModel = Bootstrapper.ServiceProvider.GetRequiredService<SearchViewModel>();
            _searchService = Bootstrapper.ServiceProvider.GetRequiredService<SearchService>();
            // Set the content control of the navigation service
            ((NavigationService)_navigationService).ContentControl = MainContent;

            // Resolve and show the MainView as the initial view
            var mainView = Bootstrapper.ServiceProvider.GetRequiredService<MainView>();
            MainContent.Content = mainView;

            var searchBox = new SearchBox();
            searchBox.Initialize(_searchService, _navigationService);
            SearchContainer.Content = searchBox;
        }

        private void Polygon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //To move the window on mouse down
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void minimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void maximizeButton_Click(object sender, RoutedEventArgs e)
        {
            //First detect if windows is in normal state or maximized
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            //Close the App
            Close();
        }

        private void oyuncuRadioEdit_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateTo<MainPlayerView>(null);
        }

        private void teamRadioEdit_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateTo<MainTeamView>(null);
        }

        private void matchRadioEdit_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateTo<MainMatchView>(null);
        }

        private void refereeRadioEdit_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateTo<MainRefereeView>(null);
        }

        private void Home_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _navigationService.NavigateTo<MainView>(null);
        }

        private void StoreItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _navigationService.NavigateTo<CoefficientView>(null);
        }

        private void GamesItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _navigationService.NavigateTo<AnalysisView>(null);
        }
    }
}
