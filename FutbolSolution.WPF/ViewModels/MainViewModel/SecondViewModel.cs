using FutbolSolution.WPF.Services.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FutbolSolution.WPF.Utils;

namespace FutbolSolution.WPF.ViewModels.MainViewModel
{
    public class SecondViewModel : INotifyPropertyChanged
    {
        private readonly INavigationService _navigationService;

        public SecondViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            GoBackCommand = new RelayCommand<object>(OnGoBack);
        }

        private string _message;
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        public ICommand GoBackCommand { get; }

        private void OnGoBack(object _)
        {
            _navigationService.GoBack();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
