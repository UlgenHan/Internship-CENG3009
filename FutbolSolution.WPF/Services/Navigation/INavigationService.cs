using System;
using System.Windows.Controls;

namespace FutbolSolution.WPF.Services.Navigation
{
    public interface INavigationService
    {
        void NavigateTo<TView>(object parameter = null) where TView : UserControl;
        void NavigateTo(Type userControlType, object parameter = null);
        bool CanGoBack { get; }
        void GoBack();
        bool CanGoForward { get; }
        void GoForward();
    }
}
