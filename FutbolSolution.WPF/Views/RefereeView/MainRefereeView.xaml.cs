using FutbolSolution.WPF.Services.Navigation;
using FutbolSolution.WPF.ViewModels.RefereeViewModel;
using System.Windows.Controls;

namespace FutbolSolution.WPF.Views.RefereeView
{
    /// <summary>
    /// Interaction logic for MainRefereeView.xaml
    /// </summary>
    public partial class MainRefereeView : UserControl, INavigable
    {
        public MainRefereeView(MainRefereeViewModel mainRefereeViewModel)
        {
            InitializeComponent();
            DataContext = mainRefereeViewModel;
        }

        public void OnNavigatedFrom()
        {
            
        }

        public void OnNavigatedTo(object parameter)
        {
            
        }

        public void Refresh()
        {
            // Refresh the view by calling a method from the ViewModel
            var viewModel = DataContext as MainRefereeViewModel;
            viewModel?.Refresh(); // Call a refresh method if implemented
        }
    }
}
