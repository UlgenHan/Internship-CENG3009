using FutbolSolution.WPF.Services.Navigation;
using FutbolSolution.WPF.Views;
using System;
using System.Windows.Input;
using FutbolSolution.WPF.Utils;
namespace FutbolSolution.WPF.ViewModels.MainViewModel
{
    public class MainViewModel : INavigable
    {
        private readonly INavigationService _navigationService;

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            //GoToSecondViewCommand = new RelayCommand<object>(GoToSecondView);
        }

        public ICommand GoToSecondViewCommand { get; }

        

        public void OnNavigatedTo(object parameter)
        {
            // Handle any parameter if needed when navigated to this view
        }

        public void OnNavigatedFrom()
        {
            throw new NotImplementedException();
        }

        public void Refresh()
        {
            
        }
    }
}
