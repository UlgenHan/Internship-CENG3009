using FutbolSolution.WPF.Services.Navigation;
using FutbolSolution.WPF.Views.RefereeView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FutbolSolution.WPF.Windows
{
    /// <summary>
    /// Interaction logic for DarkThemeMessageBox.xaml
    /// </summary>
    public partial class DarkThemeMessageBox : Window
    {
        private INavigationService _navigationService;
        public DarkThemeMessageBox(string message,INavigationService navigationService)
        {
            InitializeComponent();
            _navigationService = navigationService;
            MessageTextBlock.Text = message;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            Close(); // Close the message box
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            // Logic to navigate back
            // This could invoke a method or trigger an event to notify the ViewModel
            Close(); // Close the message box
            _navigationService.GoBack();
            
        }
    }
}
