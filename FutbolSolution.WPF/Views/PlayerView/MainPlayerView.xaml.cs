using FutbolSolution.WPF.Services.Navigation;
using FutbolSolution.WPF.ViewModels.PlayerViewModel;
using System;
using System.Windows.Controls;

namespace FutbolSolution.WPF.Views.PlayerView
{
    /// <summary>
    /// Interaction logic for MainPlayerView.xaml
    /// </summary>
    public partial class MainPlayerView : UserControl, INavigable
    {
        private readonly INavigationService _navigationService;
        private MainPlayerViewModel _mainPlayerViewModel;
        public MainPlayerView(INavigationService navigationService,MainPlayerViewModel mainPlayerViewModel)
        {
            InitializeComponent();
            _navigationService = navigationService;
            _mainPlayerViewModel = mainPlayerViewModel;
            DataContext = _mainPlayerViewModel;
        }

        public void OnNavigatedFrom()
        {
           
        }

        public void OnNavigatedTo(object parameter)
        {
            
        }

        public void Refresh()
        {
            var mainPlayerViewModel = DataContext as MainPlayerViewModel;
            if (mainPlayerViewModel != null)
            {
                mainPlayerViewModel.Refresh();
            }
        }
    }
}
